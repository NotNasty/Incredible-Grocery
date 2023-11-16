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
            Image.sprite = product.productImage;
            
            Toggle = GetComponent<Toggle>();
            Toggle.onValueChanged.AddListener(OnToggleValueChanged);

            UpdateProduct();
        }

        private void OnToggleValueChanged(bool toggleOn)
        {
            OnCheckProduct(toggleOn);
        }

        public void UncheckProduct()
        {
            if (Toggle.isOn)
            {
                Toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
                Toggle.isOn = false;
                OnCheckProduct(false, false);
                Toggle.onValueChanged.AddListener(OnToggleValueChanged);
            }
        }

        public abstract void UpdateProduct();

        private void OnCheckProduct(bool toggleOn, bool playSound = true)
        {
            ProductClicked?.Invoke(toggleOn, Product);
            if (playSound)
            {
                AudioManager.Instance.PlaySound(AudioTypeEnum.ProductSelected);
            }
        }
    }
}
