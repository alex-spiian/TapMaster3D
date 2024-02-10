using System;
using System.Collections.Generic;
using DefaultNamespace.Items;
using UnityEngine;
using System.Linq;
using Boosters;
using DefaultNamespace.Inventory;
using Skin;

namespace DefaultNamespace.Player
{
    public class Inventory : MonoBehaviour
    {
        public event Action BoosterWasAdded;
        [SerializeField] private SkinsView _skinsView;
        private readonly List<IBooster> _boosters = new();
        private List<ISkin> _skins  = new();

        public void AddNewItem(IItem item)
        {
            if (item.Type == ItemsType.Skin)
            {
                var skin = (ISkin)item;
                skin.IsBought = true;
                _skins.Add(skin);
                _skinsView.UpdateSkinsView(skin);

                return;
            }

            var booster = (IBooster)item;
            booster.AddItem();
            BoosterWasAdded?.Invoke();
            PlayerPrefs.SetInt(booster.Type.ToString(), booster.Count);
        }
         
        public IBooster GetItemByType(ItemsType itemsType)
        {
            foreach (var item in _boosters)
            {
                if (item.Type == itemsType)
                {
                    return item;
                }
            }
            return null;
        }

        public void SpendItem(ItemsType type)
        {
            for (int i = 0; i < _boosters.Count; i++)
            {
                if (_boosters[i].Type == type)
                {
                    _boosters[i].SpendItem();
                    PlayerPrefs.SetInt(_boosters[i].Type.ToString(), _boosters[i].Count);
                    BoosterWasAdded?.Invoke();
                    return;
                }
            }
        }
        
        public void SetDefaultData()
        {
            if (_boosters.Count > 0)
            {
                _boosters.Clear();
            }
            
            _boosters.Add(new BlackHoleBooster());
            _boosters.Add(new LaserBooster());
            _boosters.Add(new RocketBooster());

            for (int i = 0; i < _boosters.Count; i++)
            {
                PlayerPrefs.SetInt(_boosters[i].Type.ToString(), _boosters[i].Count);
            }
            BoosterWasAdded?.Invoke();
        }
        
        public void LoadData()
        {
            _boosters.Add(new BlackHoleBooster());
            _boosters.Add(new LaserBooster());
            _boosters.Add(new RocketBooster());
            
            for (int i = 0; i < _boosters.Count; i++)
            {
                _boosters[i].Count = PlayerPrefs.GetInt(_boosters[i].Type.ToString());
            }
            BoosterWasAdded?.Invoke();
        }

    }
}