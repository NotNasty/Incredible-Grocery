using IncredibleGrocery.Audio;
using IncredibleGrocery.Money;
using IncredibleGrocery.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery
{
    public class MainScreen : MonoBehaviour
    {
        [SerializeField] private MoneyView moneyView;
        [SerializeField] private Button settingsButton;
        [SerializeField] private SettingsPanel settingsPanel;

        public void Init(SaveDataManager saveDataManager, AudioManager audioManager)
        {
            moneyView.Init();
            settingsPanel.Init(saveDataManager, audioManager);
            settingsButton.onClick.AddListener(OnSettingButtonClick);
        }

        private void OnSettingButtonClick()
        {
            settingsPanel.SetActive(true);
            SoundPlayer.PlayButtonClicked();
        }
    }
}