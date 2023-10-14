using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using IncredibleGrocery;

[RequireComponent(typeof(TextMeshPro))]
public class MoneyView : MonoBehaviour
{
    private TMP_Text _text;
    private const string MONEY_FORMAT = "$ {0}";

    public void Init()
    {
        _text = GetComponent<TMP_Text>();
        _text.text = string.Format(MONEY_FORMAT, 0);
    }

    private void OnEnable()
    {
        MoneyManager.BalanceChanged += ChangeMoneyBalance;
    }

    private void ChangeMoneyBalance(int moneyBalance)
    {
        _text.text = string.Format(MONEY_FORMAT, moneyBalance);
    }

    private void OnDisable()
    {
        MoneyManager.BalanceChanged += ChangeMoneyBalance;
    }
}
