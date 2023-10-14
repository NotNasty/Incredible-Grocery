using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IncredibleGrocery
{
    [CreateAssetMenu(fileName = "New Product", menuName = "Product")]
    public class ProductSO : ScriptableObject
    {
        public Sprite ProductImage;
        public int Price = 10;
    }
}