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
            var lastCompleted = PlayerPrefs.GetInt("LastCompletedLevel");
            CurrentLevelIndex++;
            
            if (PlayerPrefs.GetInt("LastCompletedLevel") <= CurrentLevelIndex)
            {
                PlayerPrefs.SetInt("LastCompletedLevel", CurrentLevelIndex);
            }

            if (CurrentLevelIndex < levelsSpawner.LevelsCount)
            {

                levelsSpawner.SpawnLevel(CurrentLevelIndex);
                PlayerPrefs.SetInt(GlobalConstants.CurrentLevel, CurrentLevelIndex);
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
            PlayerPrefs.SetInt(GlobalConstants.CurrentLevel, index - 1);
            
            Debug.Log("current level from prefs " + PlayerPrefs.GetInt(GlobalConstants.CurrentLevel));
            Debug.Log("current level" + CurrentLevelIndex);
            Debug.Log("last completed level from prefs " + PlayerPrefs.GetInt("LastCompletedLevel"));

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
            PlayerPrefs.SetInt(GlobalConstants.CurrentLevel, CurrentLevelIndex);
            PlayerPrefs.SetInt("LastCompletedLevel", CurrentLevelIndex);
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