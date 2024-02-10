using System;
using Boosters;
using DefaultNamespace.Inventory;
using UnityEngine;

namespace DefaultNamespace.Items
{
    public class LaserBooster : IBooster
    {
        [field:SerializeField]
        public ItemsType Type { get; private set; }
        [field:SerializeField]
        public int Price { get; private set; }
        public int Count { get; set; }

        public LaserBooster()
        {
            Type = ItemsType.Laser;
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
     
    }
}