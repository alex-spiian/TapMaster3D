using System;
using UnityEngine;

namespace DefaultNamespace.Items
{
    public class BoostersController : MonoBehaviour
    {
        [SerializeField] private Player.Player _player;
        [SerializeField] private GameObject _boostersParent;
        private IBooster[] _boosters;

        private void Awake()
        {
            _boosters = _boostersParent.GetComponentsInChildren<IBooster>();
            
            for (int i = 0; i < _boosters.Length; i++)
            {
                _boosters[i].WasBought += _player.TryBuy;
            }
        }
        
    }
}