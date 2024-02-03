using System;
using Cube;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private ScreensController.ScreensController _screensController;
        [SerializeField]
        private CubesController _cubesController;
        
        private void Awake()
        {
            _cubesController.LastCubeWasGone += _screensController.ShowVictoryScreen;
        }

        private void OnDestroy()
        {
            _cubesController.LastCubeWasGone += _screensController.ShowVictoryScreen;
        }
    }
}