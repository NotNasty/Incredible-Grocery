using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery
{
    public class SettingsPanel : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button saveButton;
        [SerializeField] private OffOnButton soundToggle;
        [SerializeField] private OffOnButton musicToggle;

        [Header("Sprites")]
        [SerializeField] private Sprite onSprite;
        [SerializeField] private Sprite offSprite;

        private SaveDataManager _saveDataManager;
        private SettingsData _settingsData;

        private void OnEnable()
        {
            saveButton.onClick.AddListener(OnSaveClick);
            soundToggle.SettingChanged += ChangeSoundState;
            musicToggle.SettingChanged += ChangeMusicState;
        }

        public void Init(SaveDataManager saveDataManager)
        {
            _saveDataManager = saveDataManager;
            _settingsData = _saveDataManager.GetSettingsData();
            soundToggle.Init(onSprite, offSprite, _settingsData.SoundsOn);
            musicToggle.Init(onSprite, offSprite, _settingsData.MusicOn);
            ChangeSoundState(_settingsData.SoundsOn);
            ChangeMusicState(_settingsData.MusicOn);
        }

        public void SetActive(bool activate)
        {
            if (gameObject.activeSelf != activate)
            {
                gameObject.SetActive(activate);
            }
        }

        private void ChangeSoundState(bool isOn)
        {
            _settingsData.SoundsOn = isOn;
            EventBus.Instance.OnSoundsStatusChanged(isOn);
        }

        private void ChangeMusicState(bool isOn)
        {
            _settingsData.MusicOn = isOn;
            EventBus.Instance.OnMusicStatusChanged(isOn);
        }

        private void OnSaveClick()
        {
            _saveDataManager.SaveSettingsData(_settingsData);
            SetActive(false);
            Time.timeScale = 1;
            EventBus.Instance.OnButtonClicked();
        }

        private void OnDisable()
        {
            saveButton.onClick.RemoveListener(OnSaveClick);
            soundToggle.SettingChanged -= ChangeSoundState;
            musicToggle.SettingChanged -= ChangeMusicState;
        }
    }

}
