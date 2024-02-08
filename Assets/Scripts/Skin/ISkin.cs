using DefaultNamespace.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Skin
{
    public interface ISkin : IItem
    {
        public Color Color { get; }
        public Sprite  Icon { get; }
        public bool  IsBought { get; set; }
        
    }
}