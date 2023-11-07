using System;
using System.Collections.Generic;
using System.Linq;
using IncredibleGrocery.Products;

namespace IncredibleGrocery.Storage
{
    public class StoragePresenter : IDisposable
    {
        public event Action StartSaleProducts;
        
        private readonly StorageView _storage;

        public StoragePresenter(StorageView storage)
        {
            _storage = storage;
            _storage.SellButtonClicked += HideStorage;
        }

        public int GetCountOfProducts()
        {
            return _storage.Products.Count;
        }

        public ProductSO GetProductByIndex(int index)
        {
            return _storage.Products[index];
        }

        public Dictionary<ProductSO, bool> CheckOrder(HashSet<ProductSO> order, ref int price, ref bool orderIsAllCorrect)
        {
            orderIsAllCorrect = true;
            var checkedOrder = new Dictionary<ProductSO, bool>();
            foreach (var selectedProduct in _storage.SelectedProducts)
            {
                foreach (var orderedProduct in order.Where(product => product.Equals(selectedProduct)))
                {
                    checkedOrder.Add(selectedProduct, true);
                    price += orderedProduct.price;
                    break;
                }

                if (!checkedOrder.ContainsKey(selectedProduct))
                {
                    orderIsAllCorrect = false;
                    checkedOrder.Add(selectedProduct, false);
                }
            }
            return checkedOrder;
        }

        public void ShowStorage()
        {
            _storage.ShowHideStorage(true);
            _storage.UncheckAllProducts();
        }

        private void HideStorage()
        {
            _storage.ShowHideStorage(false);
            StartSaleProducts?.Invoke();
        }

        public void Dispose()
        {
            _storage.SellButtonClicked -= HideStorage;
        }
    }
}
