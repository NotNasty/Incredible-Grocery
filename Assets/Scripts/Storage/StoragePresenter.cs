using System;
using System.Collections.Generic;
using System.Linq;
using IncredibleGrocery.Audio;
using IncredibleGrocery.ClientLogic;
using IncredibleGrocery.Products;
using IncredibleGrocery.ToggleButtons;

namespace IncredibleGrocery.Storage
{
    public class StoragePresenter : IDisposable
    {
        private readonly StorageView _view;
        private readonly List<ProductSO> _products;
        private readonly List<ProductButton> _productsButtons = new();
        private readonly List<ProductSO> _selectedProducts = new();
        
        public event Action StartSaleProducts;

        public StoragePresenter(StorageView view, List<ProductSO> products)
        {
            _view = view;
            _products = products;
            _view.SellButtonClicked += HideView;
            _view.Init();
            AddProductsButtons();
        }
        
        private void AddProductsButtons()
        {
            foreach (var product in _products)
            {
                ProductButton productButton = _view.CreateProductButton();
                productButton.SetProduct(product);
                productButton.ProductClicked += OnProductClicked;
                _productsButtons.Add(productButton);
            }
        }
        
        private void OnProductClicked(bool isSelected, ProductSO product)
        {
            if (isSelected)
            {
                _selectedProducts.Add(product);
            }
            else
            {
                _selectedProducts.Remove(product);
            }

            _view.SetSellButtonInteractable(_selectedProducts.Count == Client.ProductsInOrder);
        }

        public Dictionary<ProductSO, bool> CheckOrder(HashSet<ProductSO> order, ref int price, ref bool orderIsAllCorrect)
        {
            orderIsAllCorrect = true;
            var checkedOrder = new Dictionary<ProductSO, bool>();
            foreach (var selectedProduct in _selectedProducts)
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
            UncheckAllProducts();
            _view.ShowHideStorage(true);
        }

        private void UncheckAllProducts()
        {
            foreach (var button in _productsButtons)
            {
                button.UncheckProduct();
            }
        }

        private void HideView()
        {
            _view.ShowHideStorage(false);
            AudioManager.Instance.PlaySound(AudioTypeEnum.ButtonClicked);
            StartSaleProducts?.Invoke();
        }

        public void Dispose()
        {
            _view.SellButtonClicked -= HideView;
        }
    }
}
