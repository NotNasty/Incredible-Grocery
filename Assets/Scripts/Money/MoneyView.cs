using UnityEngine;
using TMPro;
using IncredibleGrocery;

[RequireComponent(typeof(TextMeshPro))]
public class MoneyView : MonoBehaviour
{
    private TMP_Text _text;


    public void Init()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        EventBus.Instance.BalanceChanged += ChangeMoneyBalance;
    }

    private void ChangeMoneyBalance(int moneyBalance)
    {
        _text.text = string.Format(Constants.MoneyDisplayFormat, moneyBalance);
    }

    private void OnDisable()
    {
        EventBus.Instance.BalanceChanged += ChangeMoneyBalance;
    }
}
