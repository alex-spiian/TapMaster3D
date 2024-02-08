using System;
using System.Collections.Generic;
using DefaultNamespace.Inventory;
using DefaultNamespace.Items;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Shop
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField] private Player.Player _player;

        public void TryBuyItem(string type)
        {
            if (Enum.TryParse<ItemsType>(type, out var itemsType))
            {
                _player.TryBuy(itemsType);
            }
        }
        
        public void TryBuyItem(Skin.Skin skin)
        {
            if (skin.IsBought) return;
            _player.TryBuy(skin);
        }
    }
}