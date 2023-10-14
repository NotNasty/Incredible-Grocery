using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System;
using System.ComponentModel;

namespace IncredibleGrocery
{
    [RequireComponent(typeof(Animator))]
    public class Storage : MonoBehaviour
    {
        [SerializeField] private List<ProductSO> products;
        [SerializeField] private Transform StorageGridContent;
        [SerializeField] private ProductButton ProductItemPrefab;
        private Animator _animator;
        private List<ProductButton> _productsButtons = new List<ProductButton>();
        private const string IS_ACTIVE_PARAM = "isActive";

        public int DelayOfAppearing;

        public void Init()
        {
            _animator = GetComponent<Animator>();
            AddProductsButtons();
        }

        private void OnEnable() 
        {
            _animator.SetBool(IS_ACTIVE_PARAM, true);
        }

        public List<ProductSO> Products
        {
            get => products;
            private set => products = value;
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

        public void AddProductsButtons()
        {
            foreach (ProductSO product in Products)
            {
                ProductButton productButton = Instantiate(ProductItemPrefab, StorageGridContent);
                productButton.SetProduct(product);
                _productsButtons.Add(productButton);
            }
        }

        private void OnDisable() 
        {
            _animator.SetBool(IS_ACTIVE_PARAM, false);
        }
    }
}