using IncredibleGrocery.Settings;

namespace IncredibleGrocery.Money
{
    public class MoneyManager
    {
        private readonly SaveDataManager _saveDataManager;
        private int _money;

        private int Money
        {
            get => _money;
            set
            {
                _money = value;
                _saveDataManager.SaveMoneyData(_money);
                EventBus.Instance.OnBalanceChanged(_money);
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