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
        public event Action GameWasCompleted;
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
            var lastCompletedLevel = PlayerPrefs.GetInt("LastCompletedLevel");
            CurrentLevelIndex++;

            if (CurrentLevelIndex < levelsSpawner.LevelsCount)
            {
                if (lastCompletedLevel <= CurrentLevelIndex)
                {
                    PlayerPrefs.SetInt("LastCompletedLevel", CurrentLevelIndex);
                }

                levelsSpawner.SpawnLevel(CurrentLevelIndex);
                PlayerPrefs.SetInt(GlobalConstants.CURRENT_LEVEL, CurrentLevelIndex);
                LevelWasChanged?.Invoke();
                return;
            }

            CurrentLevelIndex--;
            GameWasCompleted?.Invoke();
        }

        public bool CanLoadLevel(int index)
        {
            if (index - 1 > PlayerPrefs.GetInt("LastCompletedLevel"))
            {
                return false;
            }
            CurrentLevelIndex = index - 1;
            PlayerPrefs.SetInt(GlobalConstants.CURRENT_LEVEL, index - 1);
            levelsSpawner.SpawnLevel(index - 1);
            LevelWasChanged?.Invoke();
            return true;
        }

        public void Restart()
        {
            levelsSpawner.SpawnLevel(CurrentLevelIndex);
            LevelWasRestarted?.Invoke();
        }

        public void StartFromBeginning()
        {
            CurrentLevelIndex = 0;
            PlayerPrefs.SetInt(GlobalConstants.CURRENT_LEVEL, CurrentLevelIndex);
            PlayerPrefs.SetInt("LastCompletedLevel", CurrentLevelIndex);
            levelsSpawner.SpawnLevel(CurrentLevelIndex);
            GameWasStartedFromBeginning?.Invoke();
        }

        public void LoadCurrentLevelIndex()
        {
            CurrentLevelIndex = PlayerPrefs.GetInt(GlobalConstants.CURRENT_LEVEL);
        }

        private void OnDestroy()
        {
            PlayerPrefs.SetInt(GlobalConstants.CURRENT_LEVEL, CurrentLevelIndex);
        }
    }
}