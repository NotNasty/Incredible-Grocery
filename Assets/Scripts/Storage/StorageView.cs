using System;
using System.Collections.Generic;
using IncredibleGrocery.Audio;
using IncredibleGrocery.Products;
using IncredibleGrocery.ToggleButtons;
using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery.Storage
{
    [RequireComponent(typeof(Animator))]
    public class StorageView : MonoBehaviour
    {
        [SerializeField] private List<ProductSO> products;
        [SerializeField] private Transform storageGridContent;
        [SerializeField] private ProductButton productItemPrefab;
        [SerializeField] private Button sellButton;
        
        private Animator _animator;
        private readonly List<ProductButton> _productsButtons = new();
        
        public List<ProductSO> Products => products;
        public List<ProductSO> SelectedProducts { get; } = new();
        
        public event Action SellButtonClicked;

        private void OnEnable()
        {
            _animator.SetBool(Constants.IsActive, true);
        }

        public void Init()
        {
            _animator = GetComponent<Animator>();
            SetActive(false);
            AddProductsButtons();
            sellButton.onClick.AddListener(OnSellClick);
        }

        private void AddProductsButtons()
        {
            foreach (var product in Products)
            {
                var productButton = Instantiate(productItemPrefab, storageGridContent);
                productButton.SetProduct(product);
                productButton.ProductClicked += OnProductClicked;
                _productsButtons.Add(productButton);
            }
        }

        private void OnProductClicked(bool isSelected, ProductSO product)
        {
            if (isSelected)
            {
                SelectedProducts.Add(product);
            }
            else
            {
                SelectedProducts.Remove(product);
            }

            SetSellButtonInteractable(SelectedProducts.Count == Client.ProductsInOrder);
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
        
        private void SetSellButtonInteractable(bool isNeededCountReached)
        {
            sellButton.interactable = isNeededCountReached;
        }
        
        private void OnSellClick()
        {
            SoundPlayer.PlayButtonClicked();
            SellButtonClicked?.Invoke();
        }

        private void OnDisable()
        {
            _animator.SetBool(Constants.IsActive, false);
        }
    }
}