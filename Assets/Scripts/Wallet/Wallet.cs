using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Wallet : MonoBehaviour 
{
    public event Action<int,int> AmountMoneyUpdated;
    public event Action MoneyWasNotEnough;
    public int Money { get; private set; }


    private void Start()
    {
        Money = 1000;
        var previousValueMoney = Money;
        AmountMoneyUpdated?.Invoke(previousValueMoney, Money);
    }

    public void AddMoney(int wonMoney)
    {
        var previousValueMoney = Money;
        Money += wonMoney;
        AmountMoneyUpdated?.Invoke(previousValueMoney, Money);
    }

    public void SpendMoney(int price)
    {
        var previousValueMoney = Money;
        Money -= price;
        AmountMoneyUpdated?.Invoke(previousValueMoney, Money);
    }

    public bool HasEnoughMoney(int price)
    {
        if (Money >= price)
        {
            return true;
        }

        Debug.Log("Денег нет!!!");
        MoneyWasNotEnough?.Invoke();
        return false;
    }

    public void SetDefaultMoney()
    {
        Money = 100;
        var previousValueMoney = Money;
        AmountMoneyUpdated?.Invoke(0, Money);
    }
}

