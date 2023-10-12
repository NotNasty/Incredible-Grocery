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

        private void OnEnable() 
        {
            StoragePresenter.NeededCountOfProductsChecked += SetEnableStatus;
        }

        private void Start()
        {
            _button = GetComponent<Button>();
        }

        private void SetEnableStatus(bool isNeededCountReached)
        {
            _button.interactable = isNeededCountReached;
        }

        void OnDisable()
        {
            StoragePresenter.NeededCountOfProductsChecked -= SetEnableStatus;
        }
    }
}