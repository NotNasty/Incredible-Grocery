using System;
using IncredibleGrocery.Audio;

namespace IncredibleGrocery.Money
{
    public class MoneyManager
    {
        private int _money;
        
        public static event Action<int> BalanceChanged;

        public int Money
        {
            get => _money;
            private set
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
            if (income == 0) 
                return;
            
            Money += income;
            AudioManager.Instance.PlaySound(income > 0 ? AudioTypeEnum.MoneyEarned : AudioTypeEnum.MoneySpent);
        }
    }
}