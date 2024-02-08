using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class WalletView : MonoBehaviour 
{
    [SerializeField] private TextMeshProUGUI _moneyAmountLabel;
    private float _timeUpdateResources = 1f;
    
    public void SetAmountMoney(int previousValue, int currentMoneyValue)
    {
        //_wonMoney = money;
        UpdateAmountMoney(previousValue, currentMoneyValue);
       
    }
    public void UpdateAmountMoney(int previousMoneyValue, int currentMoneyValue)
    {
        StartCoroutine(ResourceCounterUtility.CountResources(_moneyAmountLabel, _timeUpdateResources, previousMoneyValue,
            currentMoneyValue));
    }
}