using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

namespace IncredibleGrocery
{

    public class Client : MonoBehaviour
    {
        public static event Func<OrderCloud, HashSet<ProductSO>> MakingOrder;
        public static event Action<Dictionary<ProductSO, bool>> OrderChecked;
        public static event Action LeftFromShop;

        private Vector2 _targetPositionForOrdering;
        private MoneyManager _moneyManager;

        [SerializeField] private OrderCloud cloudPrefab;
        [SerializeField] private Sprite positiveReaction;
        [SerializeField] private Sprite negativeReaction;
        [SerializeField] private ClientAnimationManager animationManager;

        private HashSet<ProductSO> _order;

        private ClientStateEnum _state;
        private bool _orderIsAllCorrect = true;
        private int _paidPrice = 0;
        private const float DESTINATION_LIMIT = .2f;

        private Vector3 _startPosition;

        private void OnEnable()
        {
            SellButton.SellButtonClicked += CheckOrder;
            Player.SaleResultRevealed += ReactAtSaleOffer;
        }

        public void Init(Vector2 targetPositionForOrdering, MoneyManager moneyManager)
        {
            animationManager.Init();
            _startPosition = transform.position;
            _moneyManager = moneyManager;
            _targetPositionForOrdering = targetPositionForOrdering;
        }

        private void Update()
        {
            switch (_state)
            {
                case ClientStateEnum.GoingToSeller:
                    _state = MoveClient(_targetPositionForOrdering) ? ClientStateEnum.Ordering : ClientStateEnum.GoingToSeller;
                    break;
                case ClientStateEnum.Ordering:
                    OrderAndWait();
                    break;
                case ClientStateEnum.Leaving:
                    if ( MoveClient(_startPosition))
                    {
                        LeftFromShop?.Invoke();
                        Destroy(gameObject);
                    }
                    break;
                default:
                    break;
            }
        }

        private bool MoveClient(Vector2 targetPosition)
        {
            transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime);
            return Vector2.Distance(transform.position, targetPosition) < DESTINATION_LIMIT;
        }

        private void OrderAndWait()
        {
            animationManager.StartWaiting();
            var cloud = Instantiate(cloudPrefab, transform);
            _order = MakingOrder?.Invoke(cloud);
            _state = ClientStateEnum.WaitingForOrder;
        }

        private void CheckOrder()
        {
            _orderIsAllCorrect = true;
            Dictionary<ProductSO, bool> checkedOrder = new Dictionary<ProductSO, bool>();
            foreach (var selectedProduct in StoragePresenter.SelectedProducts)
            {
                foreach (var orderedProduct in _order)
                {
                    if (orderedProduct.Equals(selectedProduct))
                    {
                        checkedOrder.Add(selectedProduct, true);
                        _paidPrice += orderedProduct.Price;
                        break;
                    }
                }

                if (!checkedOrder.ContainsKey(selectedProduct))
                {
                    _orderIsAllCorrect = false;
                    checkedOrder.Add(selectedProduct, false);
                }
            }
            OrderChecked?.Invoke(checkedOrder);
        }

        private async void ReactAtSaleOffer()
        {
            var cloudManager = Instantiate(cloudPrefab, transform);
            if (_orderIsAllCorrect)
            {
                cloudManager.AddReaction(positiveReaction);
                _paidPrice *= 2;
            }
            else
            {
                cloudManager.AddReaction(negativeReaction);
            }
            _moneyManager.AddToBalance(_paidPrice);
            await Task.Delay(1000);
            LeaveShop();
        }

        private void LeaveShop()
        {
            animationManager.Leaving();
            _state = ClientStateEnum.Leaving;
        }

        private void OnDisable()
        {
            SellButton.SellButtonClicked -= CheckOrder;
            Player.SaleResultRevealed -= ReactAtSaleOffer;
        }
    }

    public enum ClientStateEnum
    {
        GoingToSeller,
        Ordering,
        WaitingForOrder,
        Leaving
    }
}
