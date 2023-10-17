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
        private SettingsPanel _settings;

        public void Init(SettingsPanel settings)
        {
            _button = GetComponent<Button>();
            _settings = settings;
            _button.onClick.AddListener(OnClick);
        }

        private void OnEnable()
        {
            _button?.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _settings.SetActive(true);
            Time.timeScale = 0;
            EventBus.Instance.OnButtonClicked();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }
    }
}

