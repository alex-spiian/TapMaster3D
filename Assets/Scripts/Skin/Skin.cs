using DefaultNamespace.Inventory;
using DefaultNamespace.Items;
using DefaultNamespace.Player;
using DefaultNamespace.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace Skin
{
    public class Skin : MonoBehaviour, ISkin
    {
        [field:SerializeField]
        public ItemsType Type { get; private set; }
        [field:SerializeField]
        public int Price { get; private set; }
        [field:SerializeField]

        public Color Color { get; private set; }
        [field:SerializeField]
        public Sprite Icon { get; private set; }

        public bool IsBought { get; set; }
        
    }
}