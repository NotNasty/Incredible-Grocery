using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IncredibleGrocery.Audio;
using IncredibleGrocery.ClientLogic.States;
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

        #region State Machine
        
        private ClientStateMachine _stateMachine;
        public ClientMovingInQueue ClientMovingInQueue { get; private set; }
        public ClientOrdering ClientOrdering { get; private set; }
        public ClientWaitingInQueue ClientWaitingInQueue { get; private set; }
        public ClientWaitingForOrder ClientWaitingForOrder { get; private set; }
        public ClientLeaving ClientLeaving { get; private set; }
        public ClientGoingToSeller ClientGoingToSeller { get; private set; }
        
        #endregion

        private Vector2 _targetPosition;
        public Vector2 TargetPosition
        {
            get => _targetPosition;
            set
            {
                if (_targetPosition == value) 
                    return;
                
                _targetPosition = value;
                _stateMachine.SwitchState(ClientMovingInQueue);
            }
        }

        [SerializeField] private ClientCloud cloudPrefab;
        [SerializeField] private Sprite positiveReaction;
        [SerializeField] private Sprite negativeReaction;
        [SerializeField] private int minCountOfOrders;
        [SerializeField] private int maxCountOfOrders;
        public ClientAnimationManager animationManager;

        private HashSet<Product> _order;
        private List<Product> _products;
        private bool _orderIsAllCorrect = true;
        private StoragesManager _storagesManager;
        private int _paidPrice;

        private const int TimeOfShowingOrder = 5;

        public void Init(Vector2 targetPosition, List<Product> products, StoragesManager storagesManager, bool firstInQueue)
        {
            animationManager.Init();
            _products = products;
            _storagesManager = storagesManager;
            
            _stateMachine = new ClientStateMachine();
            ClientLeaving = new ClientLeaving(this, _stateMachine, transform.position);
            ClientOrdering = new ClientOrdering(this, _stateMachine);
            ClientWaitingInQueue = new ClientWaitingInQueue(this, _stateMachine);
            ClientMovingInQueue = new ClientMovingInQueue(this, _stateMachine);
            ClientWaitingForOrder = new ClientWaitingForOrder(this, _stateMachine);
            ClientGoingToSeller = new ClientGoingToSeller(this, _stateMachine);
            
            TargetPosition = targetPosition;
            _stateMachine.Initialize(firstInQueue ? ClientGoingToSeller : ClientMovingInQueue);
        }

        private void Update()
        {
            _stateMachine.CurrentState.UpdateState();
        }

        public void GoToSeller()
        {
            if (_stateMachine.CurrentState == ClientWaitingInQueue || _stateMachine.CurrentState == ClientMovingInQueue)
                _stateMachine.SwitchState(ClientGoingToSeller);
        }

        public bool MoveClient(Vector2 targetPosition)
        {
            transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime);
            return Vector2.Distance(transform.position, targetPosition) < Constants.DestinationToPlayerLimit;
        }

        public async void OrderAndWait()
        {
            animationManager.SetAnimation(ClientAnimationType.Waiting);
            var cloud = Instantiate(cloudPrefab, transform);
            _order = OnMakingOrder(cloud);
            _stateMachine.SwitchState(ClientWaitingForOrder);
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
        
        public void LeaveOnEndWaitingTime()
        {
            var cloudManager = Instantiate(cloudPrefab, transform);
            cloudManager.AddImage(negativeReaction);
            AudioManager.Instance.PlaySound(AudioTypeEnum.BadService);
            LeaveShop();
        }

        private void LeaveShop()
        {
            animationManager.SetAnimation(ClientAnimationType.Leaving);
            _stateMachine.SwitchState(ClientLeaving);
            LeftFromShop?.Invoke(this);
        }
    }
}
