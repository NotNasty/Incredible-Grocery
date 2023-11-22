using IncredibleGrocery.ToggleButtons;
using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery.Settings
{
    public class SettingsPanelView : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button saveButton;
        [SerializeField] private OffOnButton soundToggle;
        [SerializeField] private OffOnButton musicToggle;

        [Header("Sprites")]
        [SerializeField] private Sprite onSprite;
        [SerializeField] private Sprite offSprite;
        
        private SettingsPanelPresenter _settingsPanelPresenter;

        public SettingsButtons Init(SettingsPanelPresenter presenter, bool soundsOn, bool musicOn)
        {
            _settingsPanelPresenter = presenter;
            saveButton.onClick.AddListener(() => _settingsPanelPresenter.OnSaveClick());
            
            SetActive(false);
            
            soundToggle.Init(onSprite, offSprite, soundsOn);
            musicToggle.Init(onSprite, offSprite, musicOn);
            return new SettingsButtons(soundToggle, musicToggle);
        }

        public void SetActive(bool activate)
        {
            gameObject.SetActive(activate);
        }
    }
}
