using System;
using DefaultNamespace.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Player
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _blackHoleCount;
        [SerializeField] private TextMeshProUGUI _laserCount;
        [SerializeField] private TextMeshProUGUI _rocketsCount;
        
        [SerializeField] private Inventory _inventory;

        private void Start()
        {
           UpdateInventoryView();
           _inventory.BoosterWasAdded += UpdateInventoryView;
        }

        public void UpdateInventoryView()
        {
            UpdateItemCountView(ItemsType.BlackHole, _blackHoleCount);
            UpdateItemCountView(ItemsType.Laser, _laserCount);
            UpdateItemCountView(ItemsType.Rocket, _rocketsCount);
        }
        
        private void UpdateItemCountView(ItemsType itemType, TextMeshProUGUI countText)
        {
            var item = _inventory.GetItemByType(itemType);

            if (item == null || item.Count <= 0)
            {
                countText.text = " ";
                countText.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                countText.transform.parent.gameObject.SetActive(true);
                countText.text = item.Count.ToString();
            }
        }

        private void OnDestroy()
        {
            _inventory.BoosterWasAdded -= UpdateInventoryView;

        }
    }
}