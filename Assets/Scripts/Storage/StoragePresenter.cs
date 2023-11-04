using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IncredibleGrocery.Clouds;
using IncredibleGrocery.Products;

namespace IncredibleGrocery.Storage
{
    public class StoragePresenter : IDisposable
    {
        private readonly StorageView _storage;
        private readonly StorageModelParent _storageModel;

        public StoragePresenter(StorageView storage, StorageModelParent storageModel)
        {
            _storage = storage;
            _storageModel = storageModel;
            EventBus.Instance.SelectedProductsChanged += OnSelectProduct;
            EventBus.Instance.OrderGenerated += OnOrderEnding;
            EventBus.Instance.SellButtonClicked += HideStorage;
        }

        public int GetCountOfProducts()
        {
            return _storage.Products.Count;
        }

        public ProductSO GetProductByIndex(int index)
        {
            return _storage.Products[index];
        }

        public bool CheckOrder(HashSet<ProductSO> order, ref int price)
        {
            return _storageModel.CheckOrder(order, ref price);
        }

        private async void OnOrderEnding(ClientCloud cloudManager)
        {
            await Task.Delay(_storage.delayOfAppearing * Constants.OneSecInMilliseconds);
            _storage.SetActive(true);
            _storage.UncheckAllProducts();
            cloudManager.RemoveCloud();
        }

        private void OnSelectProduct(int countOfProducts)
        {
            EventBus.Instance.OnNeededCountOfProducts(countOfProducts == Client.ProductsInOrder);
        }

        private void HideStorage()
        {
            _storage.SetActive(false);
        }

        public void Dispose()
        {
            EventBus.Instance.SelectedProductsChanged -= OnSelectProduct;
            EventBus.Instance.OrderGenerated -= OnOrderEnding;
            EventBus.Instance.SellButtonClicked -= HideStorage;
        }
    }
}
