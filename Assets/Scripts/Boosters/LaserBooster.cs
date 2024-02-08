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
        public int Count { get; private set; }

        public LaserBooster()
        {
            Type = ItemsType.Laser;
            Count = 3;
            Price = 500;
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