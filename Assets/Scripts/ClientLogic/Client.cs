using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IncredibleGrocery.Audio;
using IncredibleGrocery.Clouds;
using IncredibleGrocery.Products;
using IncredibleGrocery.Storage;
using UnityEngine;
using Random = UnityEngine.Random;

namespace IncredibleGrocery.ClientLogic
{
    public class Client : MonoBehaviour
    {
        public event Action<Client> LeftFromShop;
        
        public static int ProductsInOrder { get; private set; }

        private Vector2 _targetPosition;
        public Vector2 TargetPosition
        {
            get => _targetPosition;
            set
            {
                if (_targetPosition == value) 
                    return;
                
                _targetPosition = value;
                State = ClientStateEnum.MovingInQueue;
            }
        }
        
        private ClientStateEnum _state;
        private ClientStateEnum State
        {
            get => _state;
            set
            {
                _state = value;
                switch (_state)
                {
                    case ClientStateEnum.MovingInQueue:
                    case ClientStateEnum.GoingToSeller:
                        animationManager.SetAnimation(ClientAnimationType.Walking);
                        break;
                    case ClientStateEnum.Ordering:
                    case ClientStateEnum.WaitingInQueue:
                    case ClientStateEnum.WaitingForOrder:
                    case ClientStateEnum.StopOrder:
                        animationManager.SetAnimation(ClientAnimationType.Waiting);
                        break;
                    case ClientStateEnum.Leaving:
                        animationManager.SetAnimation(ClientAnimationType.Walking);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        [SerializeField] private ClientCloud cloudPrefab;
        [SerializeField] private Sprite positiveReaction;
        [SerializeField] private Sprite negativeReaction;
        [SerializeField] private int minCountOfOrders;
        [SerializeField] private int maxCountOfOrders;
        [SerializeField] private ClientAnimationManager animationManager;

        private HashSet<Product> _order;
        private List<Product> _products;
        private bool _orderIsAllCorrect = true;
        private StoragesManager _storagesManager;
        private int _paidPrice;
        private Vector2 _startPosition;
        private ClientProgressBar _progressBar;
        
        private const int TimeOfShowingOrder = 3;

        public void Init(Vector2 targetPosition, List<Product> products, StoragesManager storagesManager, bool firstInQueue)
        {
            animationManager.Init();
            _products = products;
            _storagesManager = storagesManager;
            _startPosition = transform.position;
            
            _progressBar = GetComponentInChildren<ClientProgressBar>();
            _progressBar.WaitingTimeEnded += LeaveOnEndWaitingTime;
            
            TargetPosition = targetPosition;
            State = firstInQueue ? ClientStateEnum.GoingToSeller : ClientStateEnum.MovingInQueue;
        }

        private void Update()
        {
            switch (State)
            {
                case ClientStateEnum.MovingInQueue:
                    if (MoveClient(TargetPosition))
                        State = ClientStateEnum.WaitingInQueue;
                    break;
                case ClientStateEnum.Ordering:
                    OrderAndWait();
                    break;
                case ClientStateEnum.WaitingInQueue:
                    _progressBar.UpdateProgressBar();
                    break;
                case ClientStateEnum.WaitingForOrder:
                    _progressBar.UpdateProgressBar();
                    break;
                case ClientStateEnum.Leaving:
                    if (MoveClient(_startPosition))
                        Destroy(gameObject);
                    break;
                case ClientStateEnum.GoingToSeller:
                    if (MoveClient(TargetPosition))
                        State = ClientStateEnum.Ordering;
                    break;
                case ClientStateEnum.StopOrder:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void GoToSeller()
        {
            if (State is ClientStateEnum.WaitingInQueue or ClientStateEnum.MovingInQueue)
                State = ClientStateEnum.GoingToSeller;
        }

        private bool MoveClient(Vector2 targetPosition)
        {
            transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime);
            return Vector2.Distance(transform.position, targetPosition) < Constants.DestinationToPlayerLimit;
        }

        private async void OrderAndWait()
        {
            animationManager.SetAnimation(ClientAnimationType.Waiting);
            var cloud = Instantiate(cloudPrefab, transform);
            _order = OnMakingOrder(cloud);
            State = ClientStateEnum.WaitingForOrder;
            await Task.Delay(TimeOfShowingOrder * Constants.OneSecInMilliseconds);
            cloud.RemoveCloud();
        }

        private HashSet<Product> OnMakingOrder(ClientCloud cloudManager)
        {
            ProductsInOrder = Random.Range(minCountOfOrders, maxCountOfOrders + 1);
            var taken = new HashSet<Product>();
            while (taken.Count < ProductsInOrder)
            {
                int pickedUpProduct = Random.Range(0, _products.Count - 1);
                int previousTakenCount = taken.Count;
                var takenProduct = _products[pickedUpProduct];
                taken.Add(takenProduct);
                if (taken.Count > previousTakenCount)
                    cloudManager.AddImage(takenProduct.productImage);
            }
            return taken;
        }

        public Dictionary<Product, bool> CheckOrder(List<Product> selectedProducts)
        {
            return _storagesManager.SellStoragePresenter.CheckOrder(selectedProducts, 
                _order, ref _paidPrice, out _orderIsAllCorrect);
        }

        public int ReactAtSaleOffer()
        {
            var cloudManager = Instantiate(cloudPrefab, transform);
            if (_orderIsAllCorrect)
            {
                cloudManager.AddImage(positiveReaction);
                _paidPrice *= 2;
            }
            else
            {
                cloudManager.AddImage(negativeReaction);
            }
            LeaveShop();
            return _paidPrice;
        }
        
        private void LeaveOnEndWaitingTime()
        {
            var cloudManager = Instantiate(cloudPrefab, transform);
            cloudManager.AddImage(negativeReaction);
            AudioManager.Instance.PlaySound(AudioTypeEnum.BadService);
            LeaveShop();
        }

        private void LeaveShop()
        {
            animationManager.SetAnimation(ClientAnimationType.Leaving);
            State = ClientStateEnum.Leaving;
            LeftFromShop?.Invoke(this);
        }
    }
}
