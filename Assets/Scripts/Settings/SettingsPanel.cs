using IncredibleGrocery.ToggleButtons;
using UnityEngine;
using UnityEngine.UI;
using IncredibleGrocery.Audio;

namespace IncredibleGrocery.Settings
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
        private AudioManager _audioManager;
        private SettingsData _settingsData;

        private void OnEnable()
        {
            Time.timeScale = 0;
            saveButton.onClick.AddListener(OnSaveClick);
            soundToggle.SettingChanged += ChangeSoundState;
            musicToggle.SettingChanged += ChangeMusicState;
        }

        public void Init(SaveDataManager saveDataManager, AudioManager audioManager)
        {
            SetActive(false);
            
            _saveDataManager = saveDataManager;
            _audioManager = audioManager;
            
            _settingsData = _saveDataManager.GetSettingsData();
            soundToggle.Init(onSprite, offSprite, _settingsData.SoundsOn);
            musicToggle.Init(onSprite, offSprite, _settingsData.MusicOn);
            ChangeSoundState(_settingsData.SoundsOn);
            ChangeMusicState(_settingsData.MusicOn);
        }

        public void SetActive(bool activate)
        {
            gameObject.SetActive(activate);
        }

        private void ChangeSoundState(bool isOn)
        {
            _settingsData.SoundsOn = isOn;
            _audioManager.OnOffSounds(isOn);
        }

        private void ChangeMusicState(bool isOn)
        {
            _settingsData.MusicOn = isOn;
            _audioManager.OnOffMusic(isOn);
        }

        private void OnSaveClick()
        {
            _saveDataManager.SaveSettingsData(_settingsData);
            SetActive(false);
            SoundPlayer.PlayButtonClicked();
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
            saveButton.onClick.RemoveListener(OnSaveClick);
            soundToggle.SettingChanged -= ChangeSoundState;
            musicToggle.SettingChanged -= ChangeMusicState;
        }
    }

}
