using System.Collections.Generic;
using IncredibleGrocery.Audio;
using IncredibleGrocery.Money;
using IncredibleGrocery.Products;
using IncredibleGrocery.ToggleButtons.Product_Buttons;

namespace IncredibleGrocery.Storage
{
    public abstract class StoragePresenter
    {
        private readonly ProductsList _products;
        private readonly List<ProductButton> _productsButtons = new();
        
        protected StorageView View { get; }
        protected List<Product> SelectedProducts { get; } = new();
        protected MoneyManager MoneyManager { get; }
        
        protected StoragePresenter(StorageView view, ProductsList products, MoneyManager moneyManager)
        {
            View = view;
            View.Init(this);
            
            MoneyManager = moneyManager;

            _products = products;
            
            AddProductsButtons();
        }
        
        private void AddProductsButtons()
        {
            foreach (var product in _products.products)
            {
                var productButton = View.CreateProductButton();
                productButton.SetProduct(product);
                productButton.ProductClicked += OnProductClicked;
                _productsButtons.Add(productButton);
            }
        }

        protected virtual void OnProductClicked(bool isSelected, Product product)
        {
            if (isSelected)
            {
                SelectedProducts.Add(product);
            }
            else
            {
                SelectedProducts.Remove(product);
            }
        }

        public virtual void OnButtonClicked()
        {
            AudioManager.Instance.PlaySound(AudioTypeEnum.ButtonClicked);
            UpdateProductButtons();
        }

        public void UpdateProductButtons()
        {
            foreach (var button in _productsButtons)
            {
                button.UncheckProduct();
                button.UpdateProduct();
            }
        }
    }
}
