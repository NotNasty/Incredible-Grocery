using System.Collections.Generic;
using System.Threading.Tasks;
using IncredibleGrocery.ClientLogic;
using IncredibleGrocery.Clouds;
using IncredibleGrocery.Money;
using IncredibleGrocery.Products;
using IncredibleGrocery.Storage;
using IncredibleGrocery.Storage.SellStorage;
using UnityEngine;
using Zenject;

namespace IncredibleGrocery
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerCloud cloudPrefab;
        
        private SellStoragePresenter _sellStoragePresenter;
        private MoneyManager _moneyManager;
        private Client _curClient;

        public Client CurrentClient
        {
            get => _curClient;
            set
            {
                _curClient = value;
                _sellStoragePresenter.UpdateProductButtons();
            }
        }

        [Inject]
        public void Init(StoragesManager storagesManager, MoneyManager moneyManager)
        {
            _sellStoragePresenter = storagesManager.SellStoragePresenter;
            _moneyManager = moneyManager;
            
            _sellStoragePresenter.StartSaleProducts += SellAsync;
        }

        private async void SellAsync(List<Product> selectedProducts)
        {
            var checkedOrder = CurrentClient.CheckOrder(selectedProducts);
            var cloud = Instantiate(cloudPrefab, transform);
            cloud.AddSales(checkedOrder);
            await Task.Delay(Constants.OneSecInMilliseconds);
            cloud.RevealReaction();
            await Task.Delay(Constants.OneSecInMilliseconds);
            _moneyManager.AddToBalance(await CurrentClient.ReactAtSaleOfferAsync());
            await Task.Delay(Constants.OneSecInMilliseconds);
            await cloud.RemoveCloudAsync();
        }
    }
}
