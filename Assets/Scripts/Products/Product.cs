using System;
using UnityEngine;

namespace IncredibleGrocery.Products
{
    [Serializable]
    public class Product
    {
        public Sprite productImage;
        public int sellPrice = 10;
        public int count = 3;
        public int orderPrice = 5;
    }
}