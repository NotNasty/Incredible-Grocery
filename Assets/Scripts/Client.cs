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
        public static event Action<OrderCloud> OrderGenerated;
        public static event Action<Dictionary<ProductSO, bool>> OrderChecked;
        public static event Action LeftFromShop;
        public static int ProductsInOrder { get; private set; }

        [SerializeField] private OrderCloud cloudPrefab;
        [SerializeField] private Sprite positiveReaction;
        [SerializeField] private Sprite negativeReaction;
        [SerializeField] private ClientAnimationManager animationManager;
        [SerializeField] private int minCountOfOrders;
        [SerializeField] private int maxCountOfOrders;

        private HashSet<ProductSO> _order;
        private Vector2 _targetPositionForOrdering;
        private MoneyManager _moneyManager;
        private ClientStateEnum _state;
        private bool _orderIsAllCorrect = true;
        private StoragePresenter _storagePresenter;
        private int _paidPrice = 0;
        private Vector3 _startPosition;

        private void OnEnable()
        {
            SellButton.SellButtonClicked += CheckOrder;
            Player.SaleResultRevealed += ReactAtSaleOffer;
        }

        public void Init(Vector2 targetPositionForOrdering, MoneyManager moneyManager, StoragePresenter storagePresenter)
        {
            animationManager.Init();
            _startPosition = transform.position;
            _moneyManager = moneyManager;
            _storagePresenter = storagePresenter;
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
                    if (MoveClient(_startPosition))
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
            return Vector2.Distance(transform.position, targetPosition) < Constants.DestinationToPlayerLimit;
        }

        private void OrderAndWait()
        {
            animationManager.StartWaiting();
            var cloud = Instantiate(cloudPrefab, transform);
            _order = OnMakingOrder(cloud);
            OrderGenerated?.Invoke(cloud);
            _state = ClientStateEnum.WaitingForOrder;
        }

        public HashSet<ProductSO> OnMakingOrder(OrderCloud cloudManager)
        {
            ProductsInOrder = UnityEngine.Random.Range(minCountOfOrders, maxCountOfOrders + 1);
            var taken = new HashSet<ProductSO>();
            while (taken.Count < ProductsInOrder)
            {
                int pickedUpProduct = UnityEngine.Random.Range(0, _storagePresenter.GetCountOfProducts() - 1);
                int previousTakenCount = taken.Count;
                var takenProduct = _storagePresenter.GetProductByIndex(pickedUpProduct);
                taken.Add(takenProduct);
                if (taken.Count > previousTakenCount)
                    cloudManager.AddOrder(takenProduct);
            }
            return taken;
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
