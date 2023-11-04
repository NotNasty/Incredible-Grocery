using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery.Storage
{
    [RequireComponent(typeof(Button))]
    public class SellButton : MonoBehaviour
    {
        private Button _button;

        private void OnEnable() 
        {
            EventBus.Instance.NeededCountOfProducts += SetEnableStatus;
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

        private void OnClick()
        {
            EventBus.Instance.OnSellButtonClicked();
        }

        private void OnDisable()
        {
            EventBus.Instance.NeededCountOfProducts -= SetEnableStatus;
            _button.onClick.RemoveListener(OnClick);
        }
    }
}