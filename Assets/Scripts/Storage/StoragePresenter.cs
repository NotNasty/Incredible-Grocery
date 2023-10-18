using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IncredibleGrocery
{
    public class StoragePresenter : IDisposable
    {
        private StorageView _storage;
        private StorageModelParent _storageModel;

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

        private async void OnOrderEnding(OrderCloud cloudManager)
        {
            await Task.Delay(_storage.DelayOfAppearing * Constants.OneSecInMilliseconds);
            _storage.SetActive(true);
            _storage.UncheckAllProducts();
            cloudManager.RemoveCloud();
        }

        private void OnSelectProduct(int countOfProducts)
        {
            EventBus.Instance.OnNeededCountOfProducts(countOfProducts == Client.ProductsInOrder);
        }

        public void HideStorage()
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
