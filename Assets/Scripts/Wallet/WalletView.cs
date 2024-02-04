using System.Collections;
using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour 
{
    [SerializeField] private TextMeshProUGUI _moneyAmount;
    private float _startAmountMoney ;
    private float _timeUpdateResources = 1f;
    private int _wonMoney;


    public void SetAmountMoney(int money)
    {
        _wonMoney = money;
       
    }

    public void UpdateAmountMoney()
    {
        StartCoroutine(UpdateAmountMoneyCoroutine());
    }

    private IEnumerator UpdateAmountMoneyCoroutine()
    {
        var currentTime = 0f;

        while (currentTime < _timeUpdateResources)
        {
            _startAmountMoney = Mathf.Lerp(_startAmountMoney, _wonMoney,
                currentTime / _timeUpdateResources);

            currentTime += Time.deltaTime;
            _moneyAmount.text = _startAmountMoney.ToString("0");
            
            yield return null;
        }
       
    }
}