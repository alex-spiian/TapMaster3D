using System;
using UnityEngine;

namespace DefaultNamespace.Items
{
    public interface IBooster
    {
        public event Action<int, IBooster> WasBought;
        public string Type { get; }
        public int Price { get; }
        
        public void Buy();
    }
}