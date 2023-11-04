using IncredibleGrocery.Products;
using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery.Clouds
{
    public class ClientCloud : CloudBase
    {
        [SerializeField] private Image orderPrefab;

        public void AddOrder(ProductSO product)
        {
            InstantiateImage().sprite = product.productImage;
        }

        public void AddReaction(Sprite reaction)
        {
            InstantiateImage().sprite = reaction;
        }

        private Image InstantiateImage()
        {
            return Instantiate(orderPrefab, cloudGridContent);
        }
    }
}
