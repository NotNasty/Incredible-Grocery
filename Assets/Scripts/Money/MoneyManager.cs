using IncredibleGrocery.Audio;

namespace IncredibleGrocery.Money
{
    public class MoneyManager
    {
        private int _money;

        public delegate void OnBalanceChanged(int newMoneyBalance, int moneyDifference);
        public static event OnBalanceChanged BalanceChanged;

        public int Money
        {
            get => _money;
            private set
            {
                BalanceChanged?.Invoke(value, value-_money);
                _money = value;
            }
        }

        public MoneyManager(int moneyCount)
        {
            Money = moneyCount;
        }

        public void AddToBalance(int income)
        {
            if (income == 0) 
                return;
            
            Money += income;
            AudioManager.Instance.PlaySound(income > 0 ? AudioTypeEnum.MoneyEarned : AudioTypeEnum.MoneySpent);
        }
    }
}