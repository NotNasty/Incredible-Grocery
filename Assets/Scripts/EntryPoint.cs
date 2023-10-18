using System.Threading.Tasks;
using UnityEngine;

namespace IncredibleGrocery
{
    public class EntryPoint : MonoBehaviour
    {
        [Header("Client")]
        [SerializeField] private GameObject clientPrefab;
        [SerializeField] private Transform targetPositionForOrdering;

        [Header("Storage")]
        [SerializeField] private SellButton sellButton;
        [SerializeField] private StorageView storageView;

        [Header("Money")]
        [SerializeField] private MoneyView moneyView;

        [Header("UI")]
        [SerializeField] private SettingsButton settingsButton;
        [SerializeField] private SettingsPanel settings;

        [Header("Audio Manager")]
        [SerializeField] private AudioManager audioManager;

        private MoneyManager _moneyManager;
        private StoragePresenter _storagePresenter;
        private Client _client;
        private ShopStateEnum _shopState;
        private SaveDataManager _saveDataManager;

        private void Awake()
        {
            _saveDataManager = new SaveDataManager();

            Init();

            StorageModelParent storageModel = new StorageModel();
            _storagePresenter = new StoragePresenter(storageView, storageModel);
            _moneyManager = new MoneyManager(_saveDataManager);
            _shopState = ShopStateEnum.NoClient;
        }

        private void OnEnable() 
        {
            EventBus.Instance.LeftFromShop += OnClientLeft;
        }

        private void Init()
        {
            audioManager.Init();
            moneyView.Init();
            storageView.Init();
            sellButton.Init();
            settingsButton.Init(settings);
            settings.Init(_saveDataManager);
        }

        private void Update()
        {
            if (_shopState == ShopStateEnum.NoClient)
            {
                _client = Instantiate(clientPrefab).GetComponent<Client>();
                _client.Init(targetPositionForOrdering.position, _moneyManager, _storagePresenter);
                _shopState = ShopStateEnum.HaveClient;
            }
        }

        public async void OnClientLeft()
        {
            await Task.Delay(Constants.OneSecInMilliseconds);
            _shopState = ShopStateEnum.NoClient;
        }

        private void  OnDisable()
        {
            EventBus.Instance.LeftFromShop -= OnClientLeft;
        }
    }

    public enum ShopStateEnum
    {
        HaveClient,
        NoClient
    }
}
