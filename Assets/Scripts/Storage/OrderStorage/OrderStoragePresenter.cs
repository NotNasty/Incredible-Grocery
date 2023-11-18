using System.Linq;
using IncredibleGrocery.Money;
using IncredibleGrocery.Products;

namespace IncredibleGrocery.Storage.OrderStorage
{
    public class OrderStoragePresenter : StoragePresenter
    {
        private int OrderPrice => SelectedProducts.Sum(x => x.OrderPrice);
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

        public override void OnButtonClicked()
        {
            foreach (var product in  SelectedProducts)
            {
                product.Count++;
            }
            MoneyManager.AddToBalance(-OrderPrice);
            base.OnButtonClicked();
        }
    }
}
