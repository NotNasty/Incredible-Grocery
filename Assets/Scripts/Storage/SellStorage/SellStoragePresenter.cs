using System;
using System.Collections.Generic;
using System.Linq;
using IncredibleGrocery.ClientLogic;
using IncredibleGrocery.Money;
using IncredibleGrocery.Products;

namespace IncredibleGrocery.Storage.SellStorage
{
    public class SellStoragePresenter : StoragePresenter
    {
        public event Action<List<Product>> StartSaleProducts;

        public SellStoragePresenter(StorageView sellView, ProductsList products, MoneyManager moneyManager) : base(
            sellView, products, moneyManager) { }

        protected override void OnProductClicked(bool isSelected, Product product)
        {
            base.OnProductClicked(isSelected, product);
            View.SetButtonInteractable(SelectedProducts.Count == Client.ProductsInOrder);
        }

        public static Dictionary<Product, bool> CheckOrder(List<Product> selectedProducts, HashSet<Product> order, ref int price, out bool orderIsAllCorrect)
        {
            orderIsAllCorrect = true;
            var checkedOrder = new Dictionary<Product, bool>();
            foreach (var selectedProduct in selectedProducts)
            {
                selectedProduct.Count--;
                foreach (var orderedProduct in order.Where(product => product.Equals(selectedProduct)))
                {
                    checkedOrder.Add(selectedProduct, true);
                    price += orderedProduct.SellPrice;
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

        public override void OnButtonClicked()
        {
            StartSaleProducts?.Invoke(SelectedProducts);
            base.OnButtonClicked();
        }
    }
}
