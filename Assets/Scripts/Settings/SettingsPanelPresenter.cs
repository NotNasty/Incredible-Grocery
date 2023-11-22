using IncredibleGrocery.ToggleButtons;
using UnityEngine;
using IncredibleGrocery.Audio;

namespace IncredibleGrocery.Settings
{
    public class SettingsPanelPresenter : MonoBehaviour
    {
        private SettingsPanelView _view;
        private SaveDataManager _saveDataManager;
        private SettingsData _settingsData;

        private OffOnButton _soundOn;
        private OffOnButton _soundOff;
        
        private void OnEnable()
        {
            Time.timeScale = 0;
        }

        public void Init(SaveDataManager saveDataManager)
        {
            _view = GetComponent<SettingsPanelView>();
            _saveDataManager = saveDataManager;
            _settingsData = _saveDataManager.GetSettingsData();
            
            var toggleButtons = _view.Init(this, _settingsData.SoundsOn, _settingsData.MusicOn);
            _soundOn = toggleButtons.SoundsButton;
            _soundOff = toggleButtons.MusicButton;
            
            _soundOn.SettingChanged += ChangeSoundState;
            _soundOff.SettingChanged += ChangeMusicState;
            
            ChangeSoundState(_settingsData.SoundsOn);
            ChangeMusicState(_settingsData.MusicOn);
        }

        private void ChangeSoundState(bool isOn)
        {
            _settingsData.SoundsOn = isOn;
            AudioManager.Instance.OnOffSounds(_settingsData.SoundsOn);
        }

        private void ChangeMusicState(bool isOn)
        {
            _settingsData.MusicOn = isOn;
            AudioManager.Instance.OnOffMusic(_settingsData.MusicOn);
        }

        public void OnSaveClick()
        {
            _saveDataManager.SaveSettingsData(_settingsData);
            _view.SetActive(false);
            AudioManager.Instance.PlaySound(AudioTypeEnum.ButtonClicked);
        }

        public void ShowSettingPanel()
        {
            _view.SetActive(true);
        }
        
        private void OnDisable()
        {
            Time.timeScale = 1;
        }
    }

}
