using TMPro;
using UnityEngine;

namespace IncredibleGrocery.Money
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class MoneyView : MonoBehaviour
    {
        private TextMeshProUGUI _moneyText;
        
        protected CanvasGroup CanvasGroup { get; private set; }

        public virtual void Init()
        {
            _moneyText = GetComponentInChildren<TextMeshProUGUI>();
            CanvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            MoneyManager.BalanceChanged += ChangeMoneyBalance;
        }

        protected abstract void ChangeMoneyBalance(int moneyBalance, int moneyDif);
        
        protected void SetMoneyBalance(string moneyText)
        {
            _moneyText.text = moneyText;
        }

        private void OnDisable()
        {
            MoneyManager.BalanceChanged -= ChangeMoneyBalance;
        }
    }
}
