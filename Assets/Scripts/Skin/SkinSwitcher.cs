using DefaultNamespace.Inventory;
using UnityEngine;

namespace Skin
{
    public class SkinSwitcher : MonoBehaviour
    {
        public Material Material;
        public Color Color;
        
        public void SetNewSkin()
        {
            Material.color = Color;
            PlayerPrefs.SetString("CurrentSkin", gameObject.tag);
        }
    }
}