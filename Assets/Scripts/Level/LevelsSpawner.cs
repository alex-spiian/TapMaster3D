using System;
using System.Collections;
using System.Collections.Generic;
using Cube;
using DefaultNamespace.Player;
using DefaultNamespace.SoundsManager;
using DG.Tweening;
using Level;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class LevelsSpawner : MonoBehaviour
{
    public event Action<bool> LevelWasSpawned;
    public int LevelsCount => _levelsPrefabs.Length;
    public int CubesCount => _allCubesOfCurrentLevel.Length;
    [SerializeField] private LevelSpawnerConfig _levelSpawnerConfig;
    [SerializeField] private GameObject[] _levelsPrefabs;
    [SerializeField] private Transform _parentTransform;
    [SerializeField] private MovesCounter _movesCounter;
    [SerializeField] private MouseClickHandler _mouseClickHandler;
    [SerializeField] private EffectsCreator _effectsCreator;
    
    private CubeMover[] _allCubesOfCurrentLevel;
    private readonly List<Vector3> _cubesTargetPositions = new();
    private GameObject _currentLevel;
    
    public void SpawnLevel(int currentLevel)
    {
        _mouseClickHandler.ClickEnabled(false);
        
        if (currentLevel >= _levelsPrefabs.Length)
        {
            return;
        }
        
        if (_currentLevel != null)
        {
            Destroy(_currentLevel);
            _cubesTargetPositions.Clear();
        }

        _currentLevel = Instantiate(_levelsPrefabs[currentLevel], _parentTransform);
        
        _currentLevel.transform.position = _levelSpawnerConfig.SpawnPoint;
        _allCubesOfCurrentLevel = _currentLevel.GetComponentsInChildren<CubeMover>();
        
        _effectsCreator.AddTrailOnCube(_allCubesOfCurrentLevel);
        _movesCounter.InitializeMovesCount(_allCubesOfCurrentLevel.Length);
        SetStartPositionForChildren();
        
        StartCoroutine(SpawnCubesAndEnableClick());

    }
    
    private IEnumerator SpawnCubesAndEnableClick()
    {
        yield return StartCoroutine(MoveCubesInRow());

        yield return new WaitForSeconds(1f); 
        _mouseClickHandler.ClickEnabled(true);
    }
    
    private IEnumerator MoveCubesInRow()
    {
        for (int i = 0; i < _allCubesOfCurrentLevel.Length; i++)
        {
            _allCubesOfCurrentLevel[i].transform.DOLocalMove(_cubesTargetPositions[i], 1);
            var minDelay = _levelSpawnerConfig.MinDelayBetweenDrops;
            var maxDelay = _levelSpawnerConfig.MaxDelayBetweenDrops;

            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        }
    }
    

    private void SetStartPositionForChildren()
    {
        for (int i = 0; i < _allCubesOfCurrentLevel.Length; i++)
        {
            
            var startPosition = _allCubesOfCurrentLevel[i].transform.localPosition;
            if (i%2==0) 
            {
                startPosition.y += _levelSpawnerConfig.YValueToBeOutOfCamera;
            }
            else if (i%3==0)
            {
                startPosition.x += _levelSpawnerConfig.YValueToBeOutOfCamera;
            }
            else if(i%3==1)
            {
                startPosition.x -= _levelSpawnerConfig.YValueToBeOutOfCamera;
            }
            else
            {
                startPosition.y -= _levelSpawnerConfig.YValueToBeOutOfCamera;
            }
            _cubesTargetPositions.Add(_allCubesOfCurrentLevel[i].transform.localPosition);
            _allCubesOfCurrentLevel[i].transform.position = startPosition;
        }
    }
    

}