using System;

namespace IncredibleGrocery
{
    public class MoneyManager
    {
        public static event Action<int> BalanceChanged;

        private SaveDataManager _saveDataManager;
        private int _money;

        public int Money
        {
            get => _money;
            set
            {
                _money = value;
                _saveDataManager.SaveMoneyData(_money);
                BalanceChanged?.Invoke(_money);
            }
        }

        public MoneyManager(SaveDataManager saveDataManager)
        {
            _saveDataManager = saveDataManager;
            Money = saveDataManager.GetMoneyCount();
        }

        public void AddToBalance(int income)
        {
            Money += income;
            if (income > 0)
            {
                EventBus.Instance.OnMoneyPaid();
            }
        }
    }
}