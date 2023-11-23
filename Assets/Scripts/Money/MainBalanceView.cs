using Zenject;

namespace IncredibleGrocery.Money
{
    public class MainBalanceView : MoneyView
    {
        private MoneyManager _moneyManager;

        [Inject]
        public void Init(MoneyManager moneyManager)
        {
            _moneyManager = moneyManager;
            ChangeMoneyBalance(_moneyManager.Money);
        }
        
        private void OnEnable()
        {
            _moneyManager.BalanceChanged += ChangeMoneyBalance;
        }
        
        private void ChangeMoneyBalance(int moneyBalance)
        {
            SetMoneyText(string.Format(Constants.MoneyDisplayFormat, moneyBalance));
        }

        private void OnDisable()
        {
            _moneyManager.BalanceChanged -= ChangeMoneyBalance;
        }
    }
}
