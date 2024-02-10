using System;
using Boosters;
using DefaultNamespace.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Items
{
    public class BlackHoleBooster : IBooster
    {
        [field:SerializeField]
        public ItemsType Type { get; private set; }
        [field:SerializeField]
        public int Price { get; private set; }
        public int Count { get; set; }
        
        public BlackHoleBooster()
        {
            Type = ItemsType.BlackHole;
            Price = 250;
            Count = 3;
        }

        public void SpendItem()
        {
            Count--;
        }

        public void AddItem()
        {
            Count++;
        }
        
        public void Activate()
        {
            // activate
        }
    }
}