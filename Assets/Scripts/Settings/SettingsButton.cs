using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery
{
    [RequireComponent(typeof(Button))]
    public class SettingsButton : MonoBehaviour
    {
        private Button _button;
        private SettingsPanel _settingsPanel;

        public void Init(SettingsPanel settingsPanel)
        {
            _button = GetComponent<Button>();
            _settingsPanel = settingsPanel;
            _button.onClick.AddListener(OnClick);
        }

        private void OnEnable()
        {
            _button?.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _settingsPanel.SetActive(true);
            EventBus.Instance.OnButtonClicked();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }
    }
}

