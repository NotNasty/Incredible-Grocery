using UnityEngine;

namespace IncredibleGrocery.Products
{
    [CreateAssetMenu(fileName = "New Product", menuName = "Product")]
    public class ProductSO : ScriptableObject
    {
        public Sprite productImage;
        public int price = 10;
    }
}