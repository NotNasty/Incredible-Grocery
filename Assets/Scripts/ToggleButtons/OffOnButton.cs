using System;
using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery.ToggleButtons
{
    [RequireComponent(typeof(Toggle), typeof(Image))]
    public class OffOnButton : MonoBehaviour
    {
        public event Action<bool> SettingChanged;

        private Image _image;
        private Toggle _toggle;
        private Sprite _onImage;
        private Sprite _offImage;

        public void Init(Sprite onImage, Sprite offImage, bool isOn)
        {
            _image = GetComponent<Image>();
            _toggle = GetComponent<Toggle>();
            
            _onImage = onImage;
            _offImage = offImage;
            
            _toggle.isOn = isOn;
            SetOnOffImage(isOn);
            _toggle.onValueChanged.AddListener(OnToggleChecked);
        }

        private void OnToggleChecked(bool isOn)
        {
            SetOnOffImage(isOn);
            SettingChanged?.Invoke(isOn);
            EventBus.Instance.OnButtonClicked();
        }

        private void SetOnOffImage(bool isOn)
        {
            _image.sprite = isOn ? _onImage : _offImage;
        }
    }
}
