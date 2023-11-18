using System.Collections.Generic;
using System.Threading.Tasks;
using IncredibleGrocery.Products;
using IncredibleGrocery.ToggleButtons;
using UnityEngine;

namespace IncredibleGrocery.Clouds
{
    public class PlayerCloud : CloudBase
    {
        [SerializeField] private SellToggleButton salePrefab;
        
        private readonly List<SellToggleButton> _saleButtons = new();

        public void AddSales(Dictionary<Product, bool> checkedOrder)
        {
            foreach (var product in checkedOrder)
            {
                var saleButton = Instantiate(salePrefab, cloudGridContent);
                saleButton.SetSaleImages(product.Key.ProductImage, product.Value);
                _saleButtons.Add(saleButton);
            }
        }

        public async void RevealReaction()
        {
            foreach(var saleButton in _saleButtons)
            {
                await Task.Delay(Constants.OneSecInMilliseconds / 2);
                saleButton.ReactionReveal();
            }
        }
    }
}
