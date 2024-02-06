using System;
using System.Collections.Generic;
using DefaultNamespace.Items;
using UnityEngine;

namespace DefaultNamespace.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private Inventory _inventory;
        
        public void TryBuy(int price, IBooster booster)
        {
            if (!_wallet.HasEnoughMoney(price)) return;
            
            _wallet.SpendMoney(price); 
            _inventory.AddNewBooster(booster);
        }
        
    }
}