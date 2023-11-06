using System.Collections.Generic;
using System.Threading.Tasks;
using IncredibleGrocery.Clouds;
using IncredibleGrocery.Money;
using IncredibleGrocery.Products;
using IncredibleGrocery.Storage;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

namespace IncredibleGrocery
{
    public class Client : MonoBehaviour
    {
        public event Action LeftFromShop;
        public event Action<Dictionary<ProductSO, bool>> OrderChecked;
        
        public static int ProductsInOrder { get; private set; }

        [SerializeField] private ClientCloud cloudPrefab;
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
        private int _paidPrice;
        private Vector3 _startPosition;

        private const int TimeOfShowingOrder = 5;

        public void Init(Vector2 targetPositionForOrdering, MoneyManager moneyManager, StoragePresenter storagePresenter)
        {
            animationManager.Init();
            _startPosition = transform.position;
            _moneyManager = moneyManager;
            _storagePresenter = storagePresenter;
            _storagePresenter.StartSaleProducts += CheckOrder;
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
                case ClientStateEnum.WaitingForOrder:
                default:
                    break;
            }
        }

        private bool MoveClient(Vector2 targetPosition)
        {
            transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime);
            return Vector2.Distance(transform.position, targetPosition) < Constants.DestinationToPlayerLimit;
        }

        private async void OrderAndWait()
        {
            animationManager.StartWaiting();
            var cloud = Instantiate(cloudPrefab, transform);
            _order = OnMakingOrder(cloud);
            _state = ClientStateEnum.WaitingForOrder;
            await Task.Delay(TimeOfShowingOrder * Constants.OneSecInMilliseconds);
            cloud.RemoveCloud();
            _storagePresenter.ShowStorage();
        }

        private HashSet<ProductSO> OnMakingOrder(ClientCloud cloudManager)
        {
            ProductsInOrder = Random.Range(minCountOfOrders, maxCountOfOrders + 1);
            var taken = new HashSet<ProductSO>();
            while (taken.Count < ProductsInOrder)
            {
                int pickedUpProduct = Random.Range(0, _storagePresenter.GetCountOfProducts() - 1);
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
            OrderChecked?.Invoke(_storagePresenter.CheckOrder(_order, ref _paidPrice, ref _orderIsAllCorrect));
        }

        public async void ReactAtSaleOffer()
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
            await Task.Delay(Constants.OneSecInMilliseconds);
            LeaveShop();
        }

        private void LeaveShop()
        {
            animationManager.Leaving();
            _state = ClientStateEnum.Leaving;
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
