using System;
using Cube;
using DefaultNamespace;
using DefaultNamespace.SoundsManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Level
{
    public class LevelsSwitcher : MonoBehaviour
    {
        public UnityEvent GameWasCompleted;

        public int CurrentLevelIndex { get; private set; }

        [SerializeField] private LevelsSpawner levelsSpawner;
        private GameObject _currentLevel;
        [SerializeField] private CubesController _cubesController;

        private void Start()
        {
            LoadCurrentLevelIndex();
            levelsSpawner.SpawnLevel(PlayerPrefs.GetInt(GlobalConstants.CurrentLevel));
        }

        public void LoadNextLevel()
        {

            if (CurrentLevelIndex + 1 < levelsSpawner.LevelsCount)
            {
                CurrentLevelIndex++;
                PlayerPrefs.SetInt(GlobalConstants.CurrentLevel, CurrentLevelIndex);
                
                levelsSpawner.SpawnLevel(CurrentLevelIndex);
                return;
            }
            
            GameWasCompleted?.Invoke();
        }

        public void Restart()
        {
            _cubesController.Reset();
            levelsSpawner.SpawnLevel(CurrentLevelIndex);
        }

        public void StartFromBeginning()
        {
            CurrentLevelIndex = 0;
            PlayerPrefs.SetInt(GlobalConstants.CurrentLevel, CurrentLevelIndex);
            
            levelsSpawner.SpawnLevel(CurrentLevelIndex);
        }

        private void LoadCurrentLevelIndex()
        {
            if (PlayerPrefs.GetInt("RunningGame", 0) == 0)
            {
                CurrentLevelIndex = 0;
                PlayerPrefs.SetInt("RunningGame", 1);
                PlayerPrefs.Save();
            }
            else
            {
                CurrentLevelIndex = PlayerPrefs.GetInt(GlobalConstants.CurrentLevel);
            }
        }

        private void OnDestroy()
        {
            PlayerPrefs.SetInt(GlobalConstants.CurrentLevel, CurrentLevelIndex);
        }
    }
}