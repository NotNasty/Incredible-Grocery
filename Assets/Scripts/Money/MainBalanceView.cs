namespace IncredibleGrocery.Money
{
    public class MainBalanceView : MoneyView
    {
        protected override void ChangeMoneyBalance(int moneyBalance, int moneyDif)
        {
            SetMoneyBalance(string.Format(Constants.MoneyDisplayFormat, moneyBalance));
        }
    }
}
