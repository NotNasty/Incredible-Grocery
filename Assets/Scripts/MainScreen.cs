using IncredibleGrocery.Audio;
using IncredibleGrocery.Money;
using IncredibleGrocery.Settings;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace IncredibleGrocery
{
    public class MainScreen : MonoBehaviour
    {
        [SerializeField] private MainBalanceView moneyView;
        [SerializeField] private Button settingsButton;
        [SerializeField] private ToastNotificationVIew notification;
        [SerializeField] private SettingsPanelPresenter settingsPanelPresenter;

        private SettingsPanelPresenter _settingsPanel;
        
        [Inject]
        public void Init(SaveDataManager saveDataManager)
        {
            settingsPanelPresenter.Init(saveDataManager);
            moneyView.Init();
            notification.Init();
            _settingsPanel = settingsPanelPresenter;
            settingsButton.onClick.AddListener(OnSettingButtonClick);
        }

        private void OnSettingButtonClick()
        {
            _settingsPanel.ShowSettingPanel();
            AudioManager.Instance.PlaySound(AudioTypeEnum.ButtonClicked);
        }
    }
}