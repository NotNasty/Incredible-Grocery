using System.Collections.Generic;
using UnityEngine;

namespace IncredibleGrocery.Products
{
    [CreateAssetMenu(fileName = "New Products List", menuName = "Products List")]
    public class ProductsList : ScriptableObject
    {
        public List<Product> products;
    }
}
