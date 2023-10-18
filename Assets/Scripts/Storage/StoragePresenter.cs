using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace IncredibleGrocery
{
    public class StoragePresenter : IDisposable
    {
        private Storage _storage;

        public static event Action<bool> NeededCountOfProductsChecked;
        public static BindingList<ProductSO> SelectedProducts = new BindingList<ProductSO>();

        public StoragePresenter(Storage storage)
        {
            _storage = storage;
            SelectedProducts.ListChanged += OnSelectProduct;
            Client.OrderGenerated += OnOrderEnding;
            SellButton.SellButtonClicked += HideStorage;
        }

        public int GetCountOfProducts()
        {
            return _storage.Products.Count;
        }

        public ProductSO GetProductByIndex(int index)
        {
            return _storage.Products[index];
        }


        private async void OnOrderEnding(OrderCloud cloudManager)
        {
            await Task.Delay(_storage.DelayOfAppearing * 1000);
            _storage.SetActive(true);
            _storage.UncheckAllProducts();
            cloudManager.RemoveCloud();
        }

        private void OnSelectProduct(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted)
            {
                var selectedProducts = sender as BindingList<ProductSO>;
                NeededCountOfProductsChecked?.Invoke(selectedProducts?.Count == Client.ProductsInOrder);
            }
        }

        public void HideStorage()
        {
            _storage.SetActive(false);
        }

        public void Dispose()
        {
            SelectedProducts.ListChanged -= OnSelectProduct;
            Client.OrderGenerated -= OnOrderEnding;
            SellButton.SellButtonClicked -= HideStorage;
        }
    }
}
