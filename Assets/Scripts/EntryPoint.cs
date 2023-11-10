using IncredibleGrocery.Money;
using IncredibleGrocery.Settings;
using IncredibleGrocery.Storage;
using UnityEngine;

namespace IncredibleGrocery
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private ClientQueue clientQueue;

        [Header("Storage")]
        [SerializeField] private StorageView storageView;

        [Header("UI")]
        [SerializeField] private MainScreen mainScreen;

        private MoneyManager _moneyManager;
        private StoragePresenter _storagePresenter;
        private SaveDataManager _saveDataManager;
        

        private void Awake()
        {
            _saveDataManager = new SaveDataManager();
            _storagePresenter = new StoragePresenter(storageView);
            storageView.Init();
            mainScreen.Init(_saveDataManager);
            _moneyManager = new MoneyManager(_saveDataManager.GetMoneyCount());
            player.Init(_storagePresenter);
            clientQueue.Init(_moneyManager, _storagePresenter, player);
        }
    }
}
