using System;
using UnityEngine;

namespace DefaultNamespace.Items
{
    public class BlackHoleBooster : MonoBehaviour, IBooster
    {
        public event Action<int, IBooster> WasBought;
        [field:SerializeField]
        public string Type { get; private set; }
        [field:SerializeField]
        public int Price { get; private set; }
        
        
        public void Buy()
        {
            WasBought?.Invoke(Price, this);
        }
    }
}