using System;
using DefaultNamespace.Player;
using Level;
using UnityEngine;
using Wallet;

namespace DataController
{
    public class DataController : MonoBehaviour
    {
        [SerializeField] private WalletDataController _walletDataController;
        [SerializeField] private LevelsSwitcher _levelsSwitcher;
        [SerializeField] private Inventory _inventory;


        private void Start()
        {
            if (PlayerPrefs.GetInt("RunningGame", 0) == 0)
            {
                _walletDataController.SetDefaultData();
                _levelsSwitcher.StartFromBeginning();
                _inventory.SetDefaultData();

                PlayerPrefs.SetInt("RunningGame", 1);
            }
            else
            {
                _walletDataController.LoadData();
                _levelsSwitcher.LoadCurrentLevelIndex();
                _inventory.LoadData();
            }
        }
        
        public void RemoveAllData()
        {
            _levelsSwitcher.StartFromBeginning();
           _walletDataController.SetDefaultData();
           _inventory.SetDefaultData();
        }
    }
}