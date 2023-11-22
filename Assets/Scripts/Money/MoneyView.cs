using TMPro;
using UnityEngine;

namespace IncredibleGrocery.Money
{
    public abstract class MoneyView : MonoBehaviour
    {
        private TextMeshProUGUI _moneyText;

        public virtual void Init()
        {
            _moneyText = GetComponentInChildren<TextMeshProUGUI>();
        }
        
        protected void SetMoneyText(string moneyText)
        {
            _moneyText.text = moneyText;
        }
    }
}
