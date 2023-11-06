using System;
using IncredibleGrocery.Audio;

namespace IncredibleGrocery.Money
{
    public class MoneyManager
    {
        public static event Action<int> BalanceChanged;
        
        private int _money;

        private int Money
        {
            get => _money;
            set
            {
                _money = value;
                BalanceChanged?.Invoke(_money);
            }
        }

        public MoneyManager(int moneyCount)
        {
            Money = moneyCount;
        }

        public void AddToBalance(int income)
        {
            Money += income;
            if (income > 0)
            {
                SoundPlayer.PlayMoneyPaid();
            }
        }
    }
}