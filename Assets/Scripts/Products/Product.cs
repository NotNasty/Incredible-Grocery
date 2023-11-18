using System;
using UnityEngine;

namespace IncredibleGrocery.Products
{
    [Serializable]
    public class Product
    {
        [field:SerializeField] public Sprite ProductImage { get; private set; }
        [field:SerializeField] public int SellPrice { get; private set; }
        [field:SerializeField] public int OrderPrice { get; private set; }
        
        [field: NonSerialized] public int Count { get; set; } = 3;
    }
}