using System.Threading.Tasks;
using IncredibleGrocery.Money;
using IncredibleGrocery.Settings;
using IncredibleGrocery.Storage;
using UnityEngine;

namespace IncredibleGrocery
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Player player;
        
        [Header("Client")]
        [SerializeField] private Client clientPrefab;
        [SerializeField] private Transform targetPositionForOrdering;

        [Header("Storage")]
        [SerializeField] private StorageView storageView;

        [Header("UI")]
        [SerializeField] private MainScreen mainScreen;

        private MoneyManager _moneyManager;
        private StoragePresenter _storagePresenter;
        private Client _client;
        private ShopStateEnum _shopState;
        private SaveDataManager _saveDataManager;

        private void Awake()
        {
            _saveDataManager = new SaveDataManager();

            Init();
            
            _storagePresenter = new StoragePresenter(storageView);
            _moneyManager = new MoneyManager(_saveDataManager.GetMoneyCount());
            _shopState = ShopStateEnum.NoClient;
        }

        private void Init()
        {
            storageView.Init();
            mainScreen.Init(_saveDataManager);
        }

        private void Update()
        {
            if (_shopState == ShopStateEnum.NoClient)
            {
                _client = Instantiate(clientPrefab);
                _client.Init(targetPositionForOrdering.position, _moneyManager, _storagePresenter);
                _client.LeftFromShop += OnClientLeft;
                player.CurrentClient = _client;
                _shopState = ShopStateEnum.HaveClient;
            }
        }

        private async void OnClientLeft()
        {
            if (_client is not null)
                _client.LeftFromShop -= OnClientLeft;
            await Task.Delay(Constants.OneSecInMilliseconds);
            player.CurrentClient = null;
            _shopState = ShopStateEnum.NoClient;
        }
    }

    public enum ShopStateEnum
    {
        HaveClient,
        NoClient
    }
}
