namespace IncredibleGrocery.Money
{
    public class MainBalanceView : MoneyView
    {
        private void OnEnable()
        {
            MoneyManager.BalanceChanged += ChangeMoneyBalance;
        }
        
        private void ChangeMoneyBalance(int moneyBalance)
        {
            SetMoneyText(string.Format(Constants.MoneyDisplayFormat, moneyBalance));
        }

        private void OnDisable()
        {
            MoneyManager.BalanceChanged -= ChangeMoneyBalance;
        }
    }
}
