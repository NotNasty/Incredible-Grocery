using System.Collections.Generic;
using IncredibleGrocery.Products;
using IncredibleGrocery.ToggleButtons;
using UnityEngine;

namespace IncredibleGrocery.Storage
{
    [RequireComponent(typeof(Animator))]
    public class StorageView : MonoBehaviour
    {
        [SerializeField] private List<ProductSO> products;
        [SerializeField] private Transform storageGridContent;
        [SerializeField] private ProductButton productItemPrefab;
        
        private Animator _animator;
        private readonly List<ProductButton> _productsButtons = new();
        
        public int delayOfAppearing;
        public List<ProductSO> Products => products;

        private void OnEnable()
        {
            _animator.SetBool(Constants.IsActive, true);
        }

        public void Init()
        {
            _animator = GetComponent<Animator>();
            AddProductsButtons();
        }

        private void AddProductsButtons()
        {
            foreach (var product in Products)
            {
                var productButton = Instantiate(productItemPrefab, storageGridContent);
                productButton.SetProduct(product);
                _productsButtons.Add(productButton);
            }
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void UncheckAllProducts()
        {
            foreach (var button in _productsButtons)
            {
                button.UncheckProduct();
            }
        }

        private void OnDisable()
        {
            _animator.SetBool(Constants.IsActive, false);
        }
    }
}