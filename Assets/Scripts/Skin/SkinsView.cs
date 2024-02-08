using System.Collections.Generic;
using DefaultNamespace.Items;
using DefaultNamespace.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Skin
{
    public class SkinsView : MonoBehaviour
    {
        [SerializeField] private List<Button> _availableButtons = new List<Button>();
        [SerializeField] private Material _material;

        public void UpdateSkinsView(ISkin skin)
        {
            var icons = _availableButtons[0].gameObject.transform.GetComponentsInChildren<Image>();
            for (int i = 0; i < icons.Length; i++)
            {
                if (icons[i].CompareTag("Icon"))
                {
                    icons[i].sprite = skin.Icon;
                }
            }

            var skinSwitcher =_availableButtons[0].gameObject.AddComponent<SkinSwitcher>();
            skinSwitcher.Color = skin.Color;
            skinSwitcher.Material = _material;
            
            _availableButtons[0].onClick.AddListener(skinSwitcher.SetNewSkin);
            _availableButtons.RemoveAt(0);
   
        }

    }
}