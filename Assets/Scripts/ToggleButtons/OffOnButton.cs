using System;
using IncredibleGrocery.Audio;
using TMPro;
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
        private TextMeshProUGUI _onOffText;

        public void Init(Sprite onImage, Sprite offImage, bool isOn)
        {
            _image = GetComponent<Image>();
            _toggle = GetComponent<Toggle>();
            _onOffText = GetComponentInChildren<TextMeshProUGUI>();
            
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
            AudioManager.Instance.PlaySound(AudioTypeEnum.ButtonClicked);
        }

        private void SetOnOffImage(bool isOn)
        {
            _image.sprite = isOn ? _onImage : _offImage;
            _onOffText.text = isOn ? Constants.OnSettingsText : Constants.OffSettingsText;
        }
    }
}
