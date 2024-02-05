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
        public event Action LevelWasChanged;
        public event Action GameWasStartedFromBeginning;
        public event Action LevelWasRestarted;

        public int CurrentLevelIndex { get; private set; }

        [SerializeField] private LevelsSpawner levelsSpawner;
        private GameObject _currentLevel;

        private void Start()
        {
            LoadCurrentLevelIndex();
        }

        public void LoadNextLevel()
        {

            if (CurrentLevelIndex + 1 < levelsSpawner.LevelsCount)
            {
                CurrentLevelIndex++;
                PlayerPrefs.SetInt(GlobalConstants.CurrentLevel, CurrentLevelIndex);
                
                levelsSpawner.SpawnLevel(CurrentLevelIndex);
                LevelWasChanged?.Invoke();
                return;
            }
            
            GameWasCompleted?.Invoke();
        }

        public void Restart()
        {
            levelsSpawner.SpawnLevel(CurrentLevelIndex);
            LevelWasRestarted?.Invoke();
        }

        public void StartFromBeginning()
        {
            CurrentLevelIndex = 0;
            PlayerPrefs.SetInt(GlobalConstants.CurrentLevel, CurrentLevelIndex);
            levelsSpawner.SpawnLevel(CurrentLevelIndex);
            GameWasStartedFromBeginning?.Invoke();
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