using System.Collections.Generic;
using System.Threading.Tasks;
using IncredibleGrocery.ClientLogic;
using IncredibleGrocery.Clouds;
using IncredibleGrocery.Money;
using IncredibleGrocery.Products;
using IncredibleGrocery.Storage.SellStorage;
using UnityEngine;

namespace IncredibleGrocery
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerCloud cloudPrefab;
        
        private SellStoragePresenter _sellStoragePresenter;
        private MoneyManager _moneyManager;
        public Client CurrentClient { get; set; }

        public void Init(SellStoragePresenter sellStoragePresenter, MoneyManager moneyManager)
        {
            _sellStoragePresenter = sellStoragePresenter;
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
            _moneyManager.AddToBalance(CurrentClient.ReactAtSaleOffer());
            await Task.Delay(Constants.OneSecInMilliseconds);
            cloud.RemoveCloud();
        }
    }
}
