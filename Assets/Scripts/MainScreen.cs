using IncredibleGrocery.Audio;
using IncredibleGrocery.Money;
using IncredibleGrocery.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery
{
    public class MainScreen : MonoBehaviour
    {
        [SerializeField] private MainBalanceView moneyView;
        [SerializeField] private Button settingsButton;
        [SerializeField] private SettingsPanel settingsPanel;
        [SerializeField] private ToastNotificationVIew notification;

        public void Init(SaveDataManager saveDataManager)
        {
            moneyView.Init();
            notification.Init();
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