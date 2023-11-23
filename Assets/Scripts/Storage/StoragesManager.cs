using IncredibleGrocery.Money;
using IncredibleGrocery.Products;
using IncredibleGrocery.Storage.OrderStorage;
using IncredibleGrocery.Storage.SellStorage;
using UnityEngine;
using Zenject;

namespace IncredibleGrocery.Storage
{
    public class StoragesManager : MonoBehaviour
    {
        [SerializeField] private StorageView sellStorageView;
        [SerializeField] private OrderStorageView orderStorageView;

        private bool _isSellMode = true;
        
        public SellStoragePresenter SellStoragePresenter { get; private set; }
        public OrderStoragePresenter OrderStoragePresenter  { get; private set; }

        private void Start()
        {
            sellStorageView.ShowHideStorage(true);
        }

        [Inject]
        public void Init(ProductsList products, MoneyManager moneyManager)
        {
            SellStoragePresenter = new SellStoragePresenter(sellStorageView, products, moneyManager);
            sellStorageView.SwitchViews += SwitchStorages;
            
            OrderStoragePresenter = new OrderStoragePresenter(orderStorageView, products, moneyManager);
            orderStorageView.SwitchViews += SwitchStorages;
        }

        private void SwitchStorages()
        {
            _isSellMode = !_isSellMode;
            sellStorageView.ShowHideStorage(_isSellMode);
            orderStorageView.ShowHideStorage(!_isSellMode);
        }
    }
}
