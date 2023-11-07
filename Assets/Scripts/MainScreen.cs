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

        public void Init(SaveDataManager saveDataManager)
        {
            moneyView.Init();
            settingsPanel.Init(saveDataManager);
            settingsButton.onClick.AddListener(OnSettingButtonClick);
        }

        private void OnSettingButtonClick()
        {
            settingsPanel.SetActive(true);
            AudioManager.Instance.PlaySound(AudioTypeEnum.ButtonClicked);
        }
    }
}