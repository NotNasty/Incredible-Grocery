using IncredibleGrocery.Money;
using IncredibleGrocery.Products;
using IncredibleGrocery.Settings;
using IncredibleGrocery.Storage;
using UnityEngine;

namespace IncredibleGrocery
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private ProductsList products;
        [SerializeField] private Player player;
        [SerializeField] private ClientQueue clientQueue;
        [SerializeField] private StoragesManager storagesManager;
        [SerializeField] private MainScreen mainScreen;
        [SerializeField] private SettingsPanelPresenter settingsPanelPresenter;


        private MoneyManager _moneyManager;
        private StoragesManager _storagesManager;
        private SaveDataManager _saveDataManager;

        private void Awake()
        {
            _saveDataManager = new SaveDataManager();
            settingsPanelPresenter.Init(_saveDataManager);
            mainScreen.Init(settingsPanelPresenter);
            _moneyManager = new MoneyManager(_saveDataManager.GetMoneyCount());
            storagesManager.Init(products, _moneyManager);
            player.Init(storagesManager.SellStoragePresenter, _moneyManager);
            clientQueue.Init(products.products, player);
            storagesManager.ShowSellStorage();
        }
    }
}
