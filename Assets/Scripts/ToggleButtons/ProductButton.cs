using System;
using IncredibleGrocery.Audio;
using IncredibleGrocery.Products;
using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery.ToggleButtons
{
    [RequireComponent(typeof(Toggle), typeof(Image))]
    public class ProductButton : MonoBehaviour
    {
        public Action<bool, ProductSO> ProductClicked;
        
        private ProductSO _product;
        private Toggle _toggle;
        private Image _image;

        public void SetProduct(ProductSO product)
        {
            _product = product;
            
            _image = GetComponent<Image>();
            _image.sprite = product.productImage;
            
            _toggle = GetComponent<Toggle>();
            _toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        private void OnToggleValueChanged(bool toggleOn)
        {
            OnCheckProduct(toggleOn);
        }

        public void UncheckProduct()
        {
            if (_toggle.isOn)
            {
                _toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
                _toggle.isOn = false;
                OnCheckProduct(false, false);
                _toggle.onValueChanged.AddListener(OnToggleValueChanged);
            }
        }

        private void OnCheckProduct(bool toggleOn, bool playSound = true)
        {
            _image.color = _image.color.ChangeAlphaChanel(toggleOn ? Constants.InactiveImageTransparency : 1);
            ProductClicked?.Invoke(toggleOn, _product);
            if (playSound)
            {
                AudioManager.Instance.PlaySound(AudioTypeEnum.ProductSelected);
            }
        }
    }
}
