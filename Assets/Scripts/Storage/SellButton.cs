using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery
{
    [RequireComponent(typeof(Button))]
    public class SellButton : MonoBehaviour
    {
        private Button _button;
        public static event Action SellButtonClicked;

        private void OnEnable() 
        {
            StoragePresenter.NeededCountOfProductsChecked += SetEnableStatus;
            _button.onClick.AddListener(OnClick);
        }

        public void Init()
        {
            _button = GetComponent<Button>();
        }

        private void SetEnableStatus(bool isNeededCountReached)
        {
            _button.interactable = isNeededCountReached;
        }

        public void OnClick()
        {
            SellButtonClicked?.Invoke();
            EventBus.Instance.OnButtonClicked();
        }

        private void OnDisable()
        {
            StoragePresenter.NeededCountOfProductsChecked -= SetEnableStatus;
            _button.onClick.RemoveListener(OnClick);
        }
    }
}