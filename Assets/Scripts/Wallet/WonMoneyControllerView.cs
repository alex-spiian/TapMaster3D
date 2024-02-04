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
    private float _startAmountCubes;
    private float _currentTime;

    public void ShowCountingCubes()
    {
        _startAmountMoney = 0;
        _startAmountCubes = 0f;
        _moneyAmount.text = _startAmountMoney.ToString("0");
        StartCoroutine(CountingCubes());
    }
    private IEnumerator CountingCubes()
    {
        _currentTime = 0f;

        while (_currentTime < _timeUpdateResources)
        {
            _startAmountCubes = Mathf.Lerp(_startAmountCubes, _cubesController.CountCubsInTotal,
                _currentTime / _timeUpdateResources);

            _currentTime += Time.deltaTime;
            _cubesAmount.text = _startAmountCubes.ToString("0");
            
            yield return null;
        }
        StartCoroutine(CountingMoney());
    }
    private IEnumerator CountingMoney()
    {
       _currentTime = 0f;

        while (_currentTime < _timeUpdateResources)
        {
            _startAmountMoney = Mathf.Lerp(_startAmountMoney, _wonMoney,
                _currentTime / _timeUpdateResources);

            _currentTime += Time.deltaTime;
            _moneyAmount.text = _startAmountMoney.ToString("0");
            
            yield return null;
        }
    }

    public void SetWonAmountMoney(int money)
    {
        _wonMoney = money;
    }


}