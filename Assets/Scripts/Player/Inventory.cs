using System.Collections.Generic;
using DefaultNamespace.Items;
using UnityEngine;
using System.Linq; 

namespace DefaultNamespace.Player
{
    public class Inventory : MonoBehaviour
    {
        private List<IBooster> _boosters = new List<IBooster>();
        public void AddNewBooster(IBooster booster)
        {
            _boosters.Add(booster);
            Debug.Log("ты купил " + booster.Type);
        }
         
        public int GetBoosterCount(string type)
        {
            return _boosters.Count(booster => booster.Type == type);
        }
        
    }
}