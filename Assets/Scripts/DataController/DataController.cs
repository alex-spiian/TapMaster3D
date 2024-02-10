using System;
using DefaultNamespace.Player;
using Level;
using Skin;
using UnityEngine;
using Wallet;

namespace DataController
{
    public class DataController : MonoBehaviour
    {
        [SerializeField] private SkinsDataController _skinsDataController;
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
                _skinsDataController.SetDefaultData();

                PlayerPrefs.SetInt("RunningGame", 1);
            }
            else
            {
                _walletDataController.LoadData();
                _levelsSwitcher.LoadCurrentLevelIndex();
                _inventory.LoadData();
                _skinsDataController.LoadSkinsData();
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