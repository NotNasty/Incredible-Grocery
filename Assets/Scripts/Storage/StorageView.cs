using System;
using IncredibleGrocery.ToggleButtons;
using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery.Storage
{
    [RequireComponent(typeof(Animator))]
    public class StorageView : MonoBehaviour
    {
        [SerializeField] private Transform storageGridContent;
        [SerializeField] private ProductButton productItemPrefab;
        [SerializeField] private Button sellButton;
        
        private Animator _animator;
        
        public event Action SellButtonClicked;
        
        public ProductButton CreateProductButton() => Instantiate(productItemPrefab, storageGridContent);

        public void Init()
        {
            _animator = GetComponent<Animator>();
            gameObject.SetActive(true);
            sellButton.onClick.AddListener(() =>
            {
                SellButtonClicked?.Invoke();
            });
        }
        
        public void ShowHideStorage(bool show)
        {
            _animator.SetBool(Constants.IsActive, show);
        }
        
        public void SetSellButtonInteractable(bool isNeededCountReached)
        {
            sellButton.interactable = isNeededCountReached;
        }
    }
}