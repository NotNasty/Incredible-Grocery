using System.Collections.Generic;
using System.Threading.Tasks;
using IncredibleGrocery.Clouds;
using IncredibleGrocery.Products;
using UnityEngine;

namespace IncredibleGrocery
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerCloud cloudPrefab;

        private Client _currentClient;
        public Client CurrentClient
        {
            get => _currentClient;
            set
            {
                if (_currentClient is not null)
                    _currentClient.OrderChecked -= SellAsync;
                
                _currentClient = value;
                
                if (_currentClient is not null)
                    _currentClient.OrderChecked += SellAsync;
            }
        }

        private async void SellAsync(Dictionary<ProductSO, bool> checkedOrder)
        {
            var cloud = Instantiate(cloudPrefab, transform);
            cloud.AddSales(checkedOrder);
            await Task.Delay(Constants.OneSecInMilliseconds);
            cloud.RevealReaction();
            await Task.Delay(Constants.OneSecInMilliseconds);
            CurrentClient.ReactAtSaleOffer();
            await Task.Delay(Constants.OneSecInMilliseconds);
            cloud.RemoveCloud();
        }
    }
}
