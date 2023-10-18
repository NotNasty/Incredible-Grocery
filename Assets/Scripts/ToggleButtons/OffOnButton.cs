using System;
using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery
{
    [RequireComponent(typeof(Toggle), typeof(Image))]
    public class OffOnButton : MonoBehaviour
    {
        public event Action<bool> SettingChanged;

        private Image _image;
        private Toggle _toggle;
        private Sprite _onImage;
        private Sprite _offImage;


        private void OnEnable()
        {
            _toggle?.onValueChanged.AddListener(OnToggleChecked);
        }

        public void Init(Sprite onImage, Sprite offImage, bool isOn)
        {
            _image = GetComponent<Image>();
            _toggle = GetComponent<Toggle>();
            _onImage = onImage;
            _offImage = offImage;
            _toggle.isOn = isOn;
            _image.sprite = isOn ? _onImage : _offImage;
        }

        public void OnToggleChecked(bool isOn)
        {
            _image.sprite = isOn ? _onImage : _offImage;
            SettingChanged?.Invoke(isOn);
            EventBus.Instance.OnButtonClicked();
        }

        public bool IsOn()
        {
            return _toggle.isOn;
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(OnToggleChecked);
        }
    }
}
