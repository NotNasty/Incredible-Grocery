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
        [SerializeField] private Storage storageView;

        [Header("Money")]
        [SerializeField] private MoneyView moneyView;

        [Header("UI")]
        [SerializeField] private SettingsButton settingsButton;
        [SerializeField] private SettingsPanel settingsPanel;

        [Header("Audio Manager")]
        [SerializeField] private AudioManager audioManager;

        private MoneyManager _moneyManager;
        private StoragePresenter _storagePresenter;
        private Client _client;
        private ShopStateEnum _shopState;

        private void Awake()
        {
            Init();
            _storagePresenter = new StoragePresenter(storageView);
            _moneyManager = new MoneyManager();
            _shopState = ShopStateEnum.NoClient;
            audioManager.PlayMusic();
        }

        private void OnEnable() 
        {
            Client.LeftFromShop += OnClientLeft;
        }

        private void Init()
        {
            audioManager.Init();
            moneyView.Init();
            storageView.Init();
            sellButton.Init();
            settingsPanel.Init();
            settingsButton.Init(settingsPanel);
        }

        private void Update()
        {
            if (_shopState == ShopStateEnum.NoClient)
            {
                _client = Instantiate(clientPrefab).GetComponent<Client>();
                _client.Init(targetPositionForOrdering.position, _moneyManager);
                _shopState = ShopStateEnum.HaveClient;
            }
            
        }

        public async void OnClientLeft()
        {
            await Task.Delay(1000);
            _shopState = ShopStateEnum.NoClient;
        }

        private void  OnDisable()
        {
            Client.LeftFromShop -= OnClientLeft;
        }
    }

    public enum ShopStateEnum
    {
        HaveClient,
        NoClient
    }
}
