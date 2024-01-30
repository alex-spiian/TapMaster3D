using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level
{
    public class LevelsController : MonoBehaviour
    {
        [SerializeField] private GameObject[] _levelsPrefabs;
        [SerializeField] private Transform _spawnPoint;

        private int _currentLevelIndex = 0;
        private GameObject _currentLevel;

        private void Awake()
        {
            _currentLevel = Instantiate(_levelsPrefabs[_currentLevelIndex]);
        }

        public void LoadNextLevel()
        {
            Debug.Log("You won!");
            _currentLevelIndex++;
            Destroy(_currentLevel);
            _currentLevel = Instantiate(_levelsPrefabs[_currentLevelIndex]);
        }

        public void Restart()
        {
            Destroy(_currentLevel);
            _currentLevel = Instantiate(_levelsPrefabs[_currentLevelIndex]);
        }
    }
}