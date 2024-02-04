using System.Collections;
using Cube;
using TMPro;
using UnityEngine;

public class WonMoneyControllerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyAmount;
    [SerializeField] private TextMeshProUGUI _cubesAmount;

    [SerializeField] private CubesController _cubesController;
    [SerializeField] private float _timeUpdateResources;

    private int _wonMoney;
    private float _startAmountMoney;

    public void ShowCountingCubes()
    {
        _startAmountMoney = 0;
        _moneyAmount.text = _startAmountMoney.ToString("0");
        StartCoroutine(CountingCubes());
    }

    private IEnumerator CountingCubes()
    {
        var currentTime = 0f;
        var startAmountCubes = 0f;

        while (currentTime < _timeUpdateResources)
        {
            startAmountCubes = Mathf.Lerp(startAmountCubes, _cubesController.CountCubsInTotal,
                currentTime / _timeUpdateResources);

            currentTime += Time.deltaTime;
            _cubesAmount.text = startAmountCubes.ToString("0");
            
            yield return null;
        }
        StartCoroutine(CountingMoney());
    }
    private IEnumerator CountingMoney()
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

    public void SetWonAmountMoney(int money)
    {
        _wonMoney = money;
    }


}