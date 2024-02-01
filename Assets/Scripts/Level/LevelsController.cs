using System;
using DefaultNamespace.SoundsManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Level
{
    public class LevelsController : MonoBehaviour
    {
        public UnityEvent GameWasCompleted;

        [SerializeField] private GameObject[] _levelsPrefabs;
        private int _currentLevelIndex;
        private GameObject _currentLevel;

        private void Awake()
        {
            _currentLevel = Instantiate(_levelsPrefabs[_currentLevelIndex]);
        }
        
        public void LoadNextLevel()
        {
            PlayerPrefs.SetInt("LastCompletedLevel", _currentLevelIndex);
            
            _currentLevelIndex++;
            if (_currentLevelIndex == _levelsPrefabs.Length)
            {
                GameWasCompleted?.Invoke();
                return;
            }
            
            Destroy(_currentLevel);
            _currentLevel = Instantiate(_levelsPrefabs[_currentLevelIndex]);
        }

        public void Restart()
        {
            Destroy(_currentLevel);
            _currentLevel = Instantiate(_levelsPrefabs[_currentLevelIndex]);
        }

        public void StartFromBeginning()
        {
            _currentLevelIndex = 0;
            PlayerPrefs.SetInt("LastCompletedLevel", _currentLevelIndex);
            Destroy(_currentLevel);
            _currentLevel = Instantiate(_levelsPrefabs[PlayerPrefs.GetInt("LastCompletedLevel")]);
            
        }
    }
}