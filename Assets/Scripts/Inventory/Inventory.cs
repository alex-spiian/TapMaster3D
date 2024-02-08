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
        public List<IBooster> Boosters { get; private set; } = new List<IBooster>();
        public List<ISkin> Skins { get; private set; } = new List<ISkin>();
        
        private void Start()
        {
            Boosters.Add(new BlackHoleBooster());
            Boosters.Add(new LaserBooster());
            Boosters.Add(new RocketBooster());
            //SetDefaultBoosters();
        }


        public void AddNewItem(IItem item)
        {
            if (item.Type == ItemsType.Skin)
            {
                var skin = (ISkin)item;
                skin.IsBought = true;
                Skins.Add(skin);
                _skinsView.UpdateSkinsView(skin);
                Debug.Log("ты купил " + item.Type);

                return;
            }

            var booster = (IBooster)item;
            booster.AddItem();
            BoosterWasAdded?.Invoke();
            Debug.Log("ты купил " + booster.Type);
        }
         
        public IBooster GetItemByType(ItemsType itemsType)
        {
            foreach (var item in Boosters)
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
            for (int i = 0; i < Boosters.Count; i++)
            {
                if (Boosters[i].Type == type)
                {
                    Boosters[i].SpendItem();
                    BoosterWasAdded?.Invoke();
                    return;
                }
            }
        }

    }
}