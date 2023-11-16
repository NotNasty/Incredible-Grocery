using System.Collections.Generic;
using System.Linq;
using IncredibleGrocery.Money;
using IncredibleGrocery.Products;

namespace IncredibleGrocery.Storage.OrderStorage
{
    public class OrderStoragePresenter : StoragePresenter
    {
        private int OrderPrice => SelectedProducts.Sum(x => x.orderPrice);
        private readonly OrderStorageView _orderView;

        public OrderStoragePresenter(OrderStorageView orderView, ProductsList products, MoneyManager moneyManager) :
            base(orderView, products, moneyManager)
        {
            _orderView =  View as OrderStorageView;
        }

        protected override void OnProductClicked(bool isSelected, Product product)
        {
            base.OnProductClicked(isSelected, product);
            var isEnoughMoney = OrderPrice <= MoneyManager.Money;
            _orderView.SetOrderPriceText(OrderPrice, isEnoughMoney);
            View.SetButtonInteractable(SelectedProducts.Count > 0 && isEnoughMoney);
        }

        public Dictionary<Product, bool> CheckOrder(List<Product> selectedProducts, HashSet<Product> order, ref int price, out bool orderIsAllCorrect)
        {
            orderIsAllCorrect = true;
            var checkedOrder = new Dictionary<Product, bool>();
            foreach (var selectedProduct in selectedProducts)
            {
                selectedProduct.count--;
                foreach (var orderedProduct in order.Where(product => product.Equals(selectedProduct)))
                {
                    checkedOrder.Add(selectedProduct, true);
                    price += orderedProduct.sellPrice;
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
            foreach (var product in  SelectedProducts)
            {
                product.count++;
            }
            MoneyManager.AddToBalance(-OrderPrice);
            base.OnButtonClicked();
        }
    }
}
