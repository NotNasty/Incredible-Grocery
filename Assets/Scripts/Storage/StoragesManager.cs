using IncredibleGrocery.Money;
using IncredibleGrocery.Products;
using IncredibleGrocery.Storage.OrderStorage;
using IncredibleGrocery.Storage.SellStorage;
using UnityEngine;

namespace IncredibleGrocery.Storage
{
    public class StoragesManager : MonoBehaviour
    {
        [SerializeField] private StorageView sellStorageView;
        [SerializeField] private OrderStorageView orderStorageView;

        private bool _isSellMode = true;
        
        public SellStoragePresenter SellStoragePresenter { get; private set; }
        public OrderStoragePresenter OrderStoragePresenter  { get; private set; }
        
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

        public void ShowSellStorage()
        {
            sellStorageView.ShowHideStorage(true);
        }
    }
}
