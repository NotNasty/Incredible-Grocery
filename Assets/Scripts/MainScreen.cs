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
        [SerializeField] private ToastNotificationVIew notification;

        private SettingsPanelPresenter _settingsPanel;
        
        public void Init(SettingsPanelPresenter settingsPanelView)
        {
            moneyView.Init();
            notification.Init();
            _settingsPanel = settingsPanelView;
            settingsButton.onClick.AddListener(OnSettingButtonClick);
        }

        private void OnSettingButtonClick()
        {
            _settingsPanel.ShowSettingPanel();
            AudioManager.Instance.PlaySound(AudioTypeEnum.ButtonClicked);
        }
    }
}