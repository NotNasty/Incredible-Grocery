using System;
using IncredibleGrocery.Audio;
using IncredibleGrocery.Products;
using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery.ToggleButtons.Product_Buttons
{
    [RequireComponent(typeof(Toggle), typeof(Image))]
    public abstract class ProductButton : MonoBehaviour
    {
        public Action<bool, Product> ProductClicked;
        
        protected Product Product;
        protected Toggle Toggle;
        protected Image Image;

        public void SetProduct(Product product)
        {
            Product = product;

            Image = GetComponent<Image>();
            Image.sprite = product.ProductImage;

            Toggle = GetComponent<Toggle>();
            Toggle.onValueChanged.AddListener(toggleOn => 
            {
                ProductClicked?.Invoke(toggleOn, Product);
                AudioManager.Instance.PlaySound(AudioTypeEnum.ProductSelected);
            });

            UpdateProduct();
        }

        public void UncheckProduct()
        {
            if (!Toggle.isOn)
                return;
            
            Toggle.SetIsOnWithoutNotify(false);
            ProductClicked?.Invoke(false, Product);
        }

        public abstract void UpdateProduct();
    }
}
