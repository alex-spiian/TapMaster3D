using System;
using UnityEngine;

public class Wallet : MonoBehaviour 
{
    private int _moneyAmount;
    public event Action<int> AmountMoneyUpdated;

    public void UpdateAmountMoney(int money)
    {
        _moneyAmount += money;
        AmountMoneyUpdated?.Invoke(_moneyAmount);
    }
}

