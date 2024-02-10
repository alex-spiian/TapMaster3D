using System;
using UnityEngine;

namespace Wallet
{
    [Serializable]
    public class WalletDataController : MonoBehaviour
    {
        [SerializeField] private int _defaultMoneyValue;
        [SerializeField] private Wallet _wallet;
        
        public void SetDefaultData()
        {
            _wallet.SetDefaultMoney(_defaultMoneyValue);
        }
        
        public void LoadData()
        {
            _wallet.AddMoney(PlayerPrefs.GetInt("Money"));
        }

        public void SaveData(int money)
        {
            PlayerPrefs.SetInt("Money", money);
        }

        private void OnDestroy()
        {
            var money = PlayerPrefs.GetInt("Money");
            PlayerPrefs.SetInt("Money", _wallet.Money);
        }
    }
}