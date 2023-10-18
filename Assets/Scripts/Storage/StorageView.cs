using UnityEngine;
using System.Collections.Generic;

namespace IncredibleGrocery
{
    [RequireComponent(typeof(Animator))]
    public class StorageView : MonoBehaviour
    {
        [SerializeField] private List<ProductSO> products;
        [SerializeField] private Transform storageGridContent;
        [SerializeField] private ProductButton productItemPrefab;
        private Animator _animator;
        private List<ProductButton> _productsButtons = new List<ProductButton>();

        public int DelayOfAppearing;
        public List<ProductSO> Products
        {
            get => products;
        }

        private void OnEnable()
        {
            _animator.SetBool(Constants.IsActiveAnimParam, true);
        }

        public void Init()
        {
            _animator = GetComponent<Animator>();
            AddProductsButtons();
        }

        public void AddProductsButtons()
        {
            foreach (ProductSO product in Products)
            {
                ProductButton productButton = Instantiate(productItemPrefab, storageGridContent);
                productButton.SetProduct(product);
                _productsButtons.Add(productButton);
            }
        }

        public void SetActive(bool enabled)
        {
            gameObject?.SetActive(enabled);
        }

        public void UncheckAllProducts()
        {
            foreach (ProductButton button in _productsButtons)
            {
                button.UncheckProduct();
            }
        }

        private void OnDisable()
        {
            _animator.SetBool(Constants.IsActiveAnimParam, false);
        }
    }
}