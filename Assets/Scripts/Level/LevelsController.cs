using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level
{
    public class LevelsController : MonoBehaviour
    {
        [SerializeField] private GameObject[] _levelsPrefabs;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField]private AudioClip _soundToPlay; // Звук для воспроизведения

        private AudioSource _audioSource;
        private int _currentLevelIndex = 1;
        private GameObject _currentLevel;

        private void Awake()
        {
            _currentLevel = Instantiate(_levelsPrefabs[_currentLevelIndex]);
        }
        
        private void Start()
        {
            // Получаем компонент AudioSource на текущем объекте
            _audioSource = GetComponent<AudioSource>();

            // Устанавливаем звук для AudioSource
            _audioSource.clip = _soundToPlay;
        }

        public void LoadNextLevel()
        {
            Debug.Log("You won!");
            _currentLevelIndex++;
            Destroy(_currentLevel);
            _currentLevel = Instantiate(_levelsPrefabs[_currentLevelIndex]);
            _audioSource.Play();
        }

        public void Restart()
        {
            Destroy(_currentLevel);
            _currentLevel = Instantiate(_levelsPrefabs[_currentLevelIndex]);
        }
    }
}