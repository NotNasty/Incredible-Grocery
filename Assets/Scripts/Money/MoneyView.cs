using TMPro;
using UnityEngine;

namespace IncredibleGrocery.Money
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class MoneyView : MonoBehaviour
    {
        private TextMeshProUGUI _moneyText;

        public void Init()
        {
            _moneyText = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            MoneyManager.BalanceChanged += ChangeMoneyBalance;
        }

        private void ChangeMoneyBalance(int moneyBalance)
        {
            _moneyText.text = string.Format(Constants.MoneyDisplayFormat, moneyBalance);
        }

        private void OnDisable()
        {
            MoneyManager.BalanceChanged += ChangeMoneyBalance;
        }
    }
}
