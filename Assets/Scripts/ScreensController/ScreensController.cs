using System;
using UnityEngine;

namespace ScreensController
{
    public class ScreensController : MonoBehaviour
    {
        [SerializeField] private Canvas _victoryScreen;
        [SerializeField] private Canvas _defeatScreen;
        [SerializeField] private Canvas _shopScreen;
        [SerializeField] private Canvas _settingsScreen;
        public event Action VictoryScreenLoaded;

        public void ShowVictoryScreen()
        {
            _victoryScreen.gameObject.SetActive(true);
            VictoryScreenLoaded?.Invoke();
        }
  
        
        public void ShowDefeatScreen()
        {
            _defeatScreen.gameObject.SetActive(true);
        }


    }
}