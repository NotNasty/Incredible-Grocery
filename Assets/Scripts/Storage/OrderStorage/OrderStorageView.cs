using TMPro;
using UnityEngine;

namespace IncredibleGrocery.Storage.OrderStorage
{
    public class OrderStorageView : StorageView
    {
        [SerializeField] private TextMeshProUGUI orderPriceText;
        [SerializeField] private Color enoughMoneyColorText;
        [SerializeField] private Color notEnoughMoneyColorText;

        public override void Init(StoragePresenter orderStoragePresenter)
        {
            base.Init(orderStoragePresenter);
            SetOrderPriceText(0, true);
        }

        public void SetOrderPriceText(int orderPrice, bool isEnoughMoney)
        {
            orderPriceText.text = string.Format(Constants.MoneyDisplayFormat, orderPrice);
            orderPriceText.color = isEnoughMoney ? enoughMoneyColorText : notEnoughMoneyColorText;
        }
    }
}