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

        private ScreensController.ScreensController _screensController;

        private void Awake()
        {
            LoadCurrentLevel();
            _currentLevel = Instantiate(_levelsPrefabs[_currentLevelIndex]);

            _screensController = Container.Instance.ScreensController;
        }
        
        public void LoadNextLevel()
        {
            PlayerPrefs.SetInt("CurrentLevel", _currentLevelIndex);
            
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
            PlayerPrefs.SetInt("CurrentLevel", _currentLevelIndex);
            Destroy(_currentLevel);
            _currentLevel = Instantiate(_levelsPrefabs[PlayerPrefs.GetInt("CurrentLevel")]);
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _screensController.ShowVictoryScreen();
            }
        }

        private void LoadCurrentLevel()
        {
            if (PlayerPrefs.GetInt("RunningGame", 0) == 0)
            {
                _currentLevelIndex = 0;
                PlayerPrefs.SetInt("RunningGame", 1);
                PlayerPrefs.Save();
            }
            else
            {
                _currentLevelIndex = PlayerPrefs.GetInt("CurrentLevel");
            }
        }

        private void OnDestroy()
        {
            PlayerPrefs.SetInt("CurrentLevel", _currentLevelIndex);
        }
    }
}