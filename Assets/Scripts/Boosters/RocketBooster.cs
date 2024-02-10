using System;
using DefaultNamespace.Inventory;
using DefaultNamespace.Items;

namespace Boosters
{
    public class RocketBooster : IBooster
    {
        public ItemsType Type { get; }
        public int Price { get; private set; }
        public int Count { get; set; }

        public RocketBooster()
        {
            Type = ItemsType.Rocket;
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