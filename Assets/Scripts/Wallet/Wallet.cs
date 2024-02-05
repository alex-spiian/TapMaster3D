using System;
using UnityEngine;

public class Wallet : MonoBehaviour 
{
    private int _allMoneyAmount;
    public event Action<int> AmountMoneyUpdated;

    public void UpdateAmountMoney(int wonMoney)
    {
        _allMoneyAmount += wonMoney;
        AmountMoneyUpdated?.Invoke(_allMoneyAmount);
    }
}

