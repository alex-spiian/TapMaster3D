using System;
using DefaultNamespace.Inventory;
using UnityEngine;

namespace DefaultNamespace.Items
{
    public interface IItem
    {
        public ItemsType Type { get; }
        public int Price { get; }
        
    }
}