using System.Collections.Generic;
using System.Threading.Tasks;
using IncredibleGrocery.ClientLogic;
using IncredibleGrocery.Clouds;
using IncredibleGrocery.Products;
using IncredibleGrocery.Storage;
using UnityEngine;

namespace IncredibleGrocery
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerCloud cloudPrefab;
        
        private StoragePresenter _storagePresenter;
        private Client _currentClient;
        public Client CurrentClient
        {
            get => _currentClient;
            set => _currentClient = value;
        }

        public void Init(StoragePresenter storagePresenter)
        {
            _storagePresenter = storagePresenter;
            
            _storagePresenter.StartSaleProducts += SellAsync;
        }

        private async void SellAsync()
        {
            var checkedOrder = _currentClient.CheckOrder();
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
