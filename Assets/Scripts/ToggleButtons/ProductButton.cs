using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery
{
    [RequireComponent(typeof(Toggle), typeof(Image))]
    public class ProductButton : MonoBehaviour
    {
        private ProductSO _product;
        private Toggle _toggle;
        private Image _image;
        private float TRANSPARENT_LEVEL = .3f;


        private void OnEnable()
        {
            _toggle?.onValueChanged.AddListener(OnToggleValueChanged);
        }

        public void SetProduct(ProductSO product)
        {
            _image = GetComponent<Image>();
            _product = product;
            _toggle = GetComponent<Toggle>();
            _image.sprite = product.ProductImage;
        }

        public void OnToggleValueChanged(bool toggleOn)
        {
            OnCheckProduct(toggleOn);
        }

        public void UncheckProduct()
        {
            if (_toggle.isOn)
            {
                _toggle?.onValueChanged.RemoveListener(OnToggleValueChanged);
                _toggle.isOn = false;
                OnCheckProduct(false, false);
                _toggle.onValueChanged.AddListener(OnToggleValueChanged);
            }
        }

        private void OnCheckProduct(bool toggleOn, bool playSound = true)
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b,
                                        toggleOn ? TRANSPARENT_LEVEL : 1);
            if (toggleOn)
            {
                StorageModel.SelectNewProduct(_product);
            }
            else
            {
                StorageModel.UnselectNewProduct(_product);
            }

            if (playSound)
            {
                EventBus.Instance.OnProductSelected();
            }
        }

        private void OnDisable()
        {
            _toggle?.onValueChanged.RemoveListener(OnToggleValueChanged);
        }
    }
}
