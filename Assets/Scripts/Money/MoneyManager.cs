using System;
using IncredibleGrocery.Audio;
using IncredibleGrocery.Settings;

namespace IncredibleGrocery.Money
{
    public class MoneyManager
    {
        private int _money;
        private SaveDataManager _saveDataManager;

        public static event Action<int> IncomeCame;
        public event Action<int> BalanceChanged;

        public int Money
        {
            get => _money;
            private set
            {
                _money = value;
                BalanceChanged?.Invoke(_money);
                _saveDataManager.SaveMoneyData(_money);
            }
        }

        public MoneyManager(SaveDataManager saveDataManager)
        {
            _saveDataManager = saveDataManager;
            Money = _saveDataManager.GetMoneyCount();
        }

        public void AddToBalance(int income)
        {
            if (income == 0) 
                return;
            
            Money += income;
            IncomeCame?.Invoke(income);
            AudioManager.Instance.PlaySound(income > 0 ? AudioTypeEnum.MoneyEarned : AudioTypeEnum.MoneySpent);
        }
    }
}