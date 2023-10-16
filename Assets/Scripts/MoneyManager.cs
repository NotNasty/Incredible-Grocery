using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IncredibleGrocery
{
    public class MoneyManager
    {
        public static event Action<int> BalanceChanged;
        private int _money;

        public int Money
        {
            get => _money;
            set
            {
                _money = value;
                BalanceChanged?.Invoke(_money);
            }
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