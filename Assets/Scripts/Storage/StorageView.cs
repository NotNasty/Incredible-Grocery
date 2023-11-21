using System;
using DG.Tweening;
using IncredibleGrocery.ToggleButtons.Product_Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery.Storage
{
    public class StorageView : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Transform storageGridContent;
        [SerializeField] private ProductButton productItemPrefab;
        [SerializeField] private Button switchModeButton;
        
        
        public event Action SwitchViews;
        private StoragePresenter _storagePresenter;
        private RectTransform _rectTransform;

        private const int ClosedPositionX = 500;
        private const int OpenedPositionX = -380;
        private const float AnimationDuration = 0.5f;
        
        public ProductButton CreateProductButton() => Instantiate(productItemPrefab, storageGridContent);

        public virtual void Init(StoragePresenter storagePresenter)
        {
            _storagePresenter = storagePresenter;
            gameObject.SetActive(true);
            switchModeButton.onClick.AddListener(() => SwitchViews?.Invoke());
            button.onClick.AddListener(() => _storagePresenter.OnButtonClicked());
            
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.anchoredPosition = new Vector2(ClosedPositionX, _rectTransform.anchoredPosition.y);
        }

        public void ShowHideStorage(bool show)
        {
            _storagePresenter.UpdateProductButtons();
            _rectTransform.DOAnchorPosX(show ? OpenedPositionX : ClosedPositionX, AnimationDuration)
                .SetEase(Ease.InOutExpo);
        }

        public void SetButtonInteractable(bool interactable)
        {
            button.interactable = interactable;
        }
    }
}