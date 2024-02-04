using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level
{
    public class LevelsController : MonoBehaviour
    {
        [SerializeField] private LevelsSpawner _levelsSpawner;
        [SerializeField] private LevelsSwitcher _levelsSwitcher;

        
        private void Awake()
        {
            _levelsSpawner.SpawnLevel(_levelsSwitcher.CurrentLevelIndex);
        }

    }
}