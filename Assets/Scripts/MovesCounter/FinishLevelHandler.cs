using System;
using Cube;
using UnityEngine;

namespace DefaultNamespace.Player
{
    public class FinishLevelHandler : MonoBehaviour
    {
        public event Action LevelWasCompleted;
        public event Action LevelWasFailed;
        
        private bool AreAllCubesGone;

        public void TryFinishLevel()
        {
            if (AreAllCubesGone)
            {
                LevelWasCompleted?.Invoke();
                AreAllCubesGone = false;
            }
            else
            {
                LevelWasFailed?.Invoke();
            }
        }

        public void OnAllCubesAreGone()
        {
            AreAllCubesGone = true;
            TryFinishLevel();
        }
    }
}