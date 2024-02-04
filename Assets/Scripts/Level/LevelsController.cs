using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level
{
    public class LevelsController : MonoBehaviour
    {
        [SerializeField] private LevelsSpawner levelsSpawner;
        [SerializeField] private LevelsSwitcher _levelsSwitcher;

        
        private void Awake()
        {
            levelsSpawner.SpawnLevel(_levelsSwitcher.CurrentLevelIndex);
        }

    }
}