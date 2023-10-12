using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System;
using System.ComponentModel;

namespace IncredibleGrocery
{
    public class Storage : MonoBehaviour
    {
        [SerializeField] private List<ProductSO> products;
        [SerializeField] private Transform StorageGridContent;
        [SerializeField] private ProductButton ProductItem;
        public int DelayOfAppearing;
        public event Action<bool> NeededCountOfProductsChecked;
        public static BindingList<ProductSO> SelectedProducts = new BindingList<ProductSO>();

        public List<ProductSO> Products
        {
            get => products;
            private set => products = value;
        }
        
        public void SetActive(bool enabled)
        {
            gameObject.SetActive(enabled);
        }

        public void AddProductsButton()
        {
            foreach (ProductSO product in Products)
            {
                ProductButton productButton = Instantiate(ProductItem, StorageGridContent);
                productButton.SetProduct(product);
            }
        }
    }
}