using System.Collections.Generic;
using UnityEngine;

namespace Skin
{
    public class SkinsDataController : MonoBehaviour
    {
        [SerializeField] private Skin[] _skins;
        [SerializeField] private SkinsView _skinsView;
        private List<ISkin> _boughtSkins = new List<ISkin>();


        public void LoadSkinsData()
        {

            for (int i = 0; i < _skins.Length; i++)
            {
                // 1 = bought
                // 0 = not bought
                
                var isBought = PlayerPrefs.GetInt(_skins[i].Name);
                if (isBought == 1)
                {
                    _boughtSkins.Add(_skins[i]);
                    _skinsView.UpdateSkinsView(_skins[i]);
                }
                
            }
            
        }
        
        public void SetDefaultData()
        {
            // _skinsView.UpdateSkinsView();
        }
    }
}