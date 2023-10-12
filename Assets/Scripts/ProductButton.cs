using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery
{
    [RequireComponent(typeof(Toggle), typeof(Image))]
    public class ProductButton : MonoBehaviour
    {
        private ProductSO _product;
        private Image _image;
        private float TRANSPARENT_LEVEL = .3f;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void SetProduct(ProductSO product)
        {
            _product = product;
            _image.sprite = product.ProductImage;
        }

        public void OnToggleValueChanged(Toggle toggle)
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b,
                                        toggle.isOn ? TRANSPARENT_LEVEL : 1);
            if (toggle.isOn)
            {
                StoragePresenter.SelectedProducts.Add(_product);
            }
            else
            {
                StoragePresenter.SelectedProducts.Remove(_product);
            }
        }
    }
}
