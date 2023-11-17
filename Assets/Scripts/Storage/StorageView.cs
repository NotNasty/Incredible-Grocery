using System;
using IncredibleGrocery.ToggleButtons.Product_Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery.Storage
{
    [RequireComponent(typeof(Animator))]
    public class StorageView : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Transform storageGridContent;
        [SerializeField] private ProductButton productItemPrefab;
        [SerializeField] private Button switchModeButton;
        
        public event Action SwitchViews;
        
        private Animator _animator;
        private StoragePresenter _storagePresenter;
        
        public ProductButton CreateProductButton() => Instantiate(productItemPrefab, storageGridContent);

        public virtual void Init(StoragePresenter storagePresenter)
        {
            _animator = GetComponent<Animator>();
            _storagePresenter = storagePresenter;
            gameObject.SetActive(true);
            switchModeButton.onClick.AddListener(() => SwitchViews?.Invoke());
            button.onClick.AddListener(() => _storagePresenter.OnButtonClicked());
        }

        public void ShowHideStorage(bool show)
        {
            _storagePresenter.UpdateProductButtons();
            _animator.SetBool(Constants.IsActive, show);
        }

        public void SetButtonInteractable(bool interactable)
        {
            button.interactable = interactable;
        }
    }
}