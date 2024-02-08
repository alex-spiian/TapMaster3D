using System;
using DefaultNamespace.Inventory;
using DefaultNamespace.Items;
using DefaultNamespace.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Boosters
{
    public class BoostersController : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private BlackHoleController blackHoleController;
        [SerializeField] private LaserShot laserShot;
        [SerializeField] private RocketShot _rocketShot;
        
        public void TryActivateBooster(string type)
        {
            if (Enum.TryParse<ItemsType>(type, out var boosterType))
            {
                var item = _inventory.GetItemByType(boosterType);
                if (item.Count <= 0) return;
                
                ActivateBooster(item);
            }
        }

        private void ActivateBooster(IItem booster)
        {
            switch (booster.Type)
            {
                case ItemsType.BlackHole:
                    if (!blackHoleController.IsActive)
                    {
                        blackHoleController.Activate();
                        _inventory.SpendItem(booster.Type);
                    }
                    break;
                case ItemsType.Laser:
                    if (!laserShot.CanShoot)
                    {
                        laserShot.ActiveShot();
                        _inventory.SpendItem(booster.Type);
                    }
                    break;
                case ItemsType.Rocket:
                    if (!_rocketShot.CanShoot)
                    {
                        _rocketShot.ActiveShot();
                        _inventory.SpendItem(booster.Type);
                    }
                    break;
                
            }
        }
    }
}