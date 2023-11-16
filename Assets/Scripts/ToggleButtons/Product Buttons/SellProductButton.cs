using TMPro;
using UnityEngine;

namespace IncredibleGrocery.ToggleButtons.Product_Buttons
{
    public class SellProductButton : ProductButton
    {
        [SerializeField] private TextMeshProUGUI countText;

        public override void UpdateProduct()
        {
            if (countText is not null)
                countText.text = string.Format(Constants.CountProductFormat, Product.count);
            
            Image.color = Image.color.ChangeAlphaChanel(Product.count > 0 ? 1 : Constants.InactiveImageTransparency);
            Toggle.interactable = Product.count > 0;
        }
    }
}
