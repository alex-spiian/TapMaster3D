using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Wallet
{
    public class Wallet : MonoBehaviour 
    {
        public event Action<int,int> AmountMoneyUpdated;
        public event Action MoneyWasNotEnough;
        public int Money { get; private set; }

        
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

       public void SetDefaultMoney(int amount)
       {
           Money = 5000;
           AmountMoneyUpdated?.Invoke(0, Money);
       }
    }

}
