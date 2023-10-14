using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using UnityEngine;

namespace IncredibleGrocery
{
    public class StoragePresenter : IDisposable
    {
        private int _productsInOrder;
        private Storage _storage;

        public static event Action<bool> NeededCountOfProductsChecked;
        public static BindingList<ProductSO> SelectedProducts = new BindingList<ProductSO>();

        public StoragePresenter(Storage storage)
        {
            _storage = storage;
            SelectedProducts.ListChanged += OnSelectProduct;
            Client.MakingOrder += OnMakingOrder;
            SellButton.SellButtonClicked += HideStorage;
        }

        public HashSet<ProductSO> OnMakingOrder(OrderCloud cloudManager)
        {
            _productsInOrder = UnityEngine.Random.Range(cloudManager.MinCountOfOrders, cloudManager.MaxCountOfOrders);
            var taken = new HashSet<ProductSO>();
            while (taken.Count < _productsInOrder)
            {
                int pickedUpProduct = UnityEngine.Random.Range(0, _storage.Products.Count - 1);
                int previousTakenCount = taken.Count;
                taken.Add(_storage.Products[pickedUpProduct]);
                if (taken.Count > previousTakenCount)
                    cloudManager.AddOrder(_storage.Products[pickedUpProduct]);
            }
            OnOrderEnding(cloudManager);
            return taken;
        }

        private async void OnOrderEnding(OrderCloud cloudManager)
        {
            await Task.Delay(_storage.DelayOfAppearing * 1000);
            //SelectedProducts.Clear();
            _storage.UncheckAllProducts();
            _storage.SetActive(true);
            cloudManager.RemoveCloud();
        }

        private void OnSelectProduct(object sender, ListChangedEventArgs e)
        {
            if (_productsInOrder> 0 && 
            e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted)
            {
                var selectedProducts = sender as BindingList<ProductSO>;
                NeededCountOfProductsChecked?.Invoke(selectedProducts is not null
                                                        && selectedProducts.Count == _productsInOrder);
            }
        }

        public void HideStorage()
        {
            _storage.SetActive(false);
        }

        public void Dispose()
        {
            SelectedProducts.ListChanged -= OnSelectProduct;
            Client.MakingOrder -= OnMakingOrder;
            SellButton.SellButtonClicked -= HideStorage;
        }
    }
}
