using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.SoundsManager;
using DG.Tweening;
using Level;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField]
    private LevelSpawnerConfig _levelSpawnerConfig;
    
    private CubeMover[] _allCubes;
    private readonly List<Vector3> _cubesTargetPositions = new();
    private int _counterOfGoneCubes;

    private ScreensController.ScreensController _screensController;
    private SoundsManager _soundsManager;
    
    private  void Awake()
    {
        _screensController = Container.Instance.ScreensController;
        _soundsManager = Container.Instance.SoundsManager;

        _allCubes = GetComponentsInChildren<CubeMover>();
        transform.position = _levelSpawnerConfig.SpawnPoint;
        
        SetStartPositionForChildren();
        StartCoroutine(MoveCubesInRow());
    }

    private void MarkCubeAsGone()
    {
        _counterOfGoneCubes++;

        if (_counterOfGoneCubes == _allCubes.Length)
        {
            _soundsManager.PlayVictory();
            _screensController.ShowVictoryScreen();
        }
    }

    private IEnumerator MoveCubesInRow()
    {
        for (int i = 0; i < _allCubes.Length; i++)
        {
            _allCubes[i].transform.DOMove(_cubesTargetPositions[i], 1);
            var minDelay = _levelSpawnerConfig.MinDelayBetweenDrops;
            var maxDelay = _levelSpawnerConfig.MaxDelayBetweenDrops;
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            
            // TODO: кидать ивент что уровень заспавнился и можно крутить фигуру 
        }
    }

    private void SetStartPositionForChildren()
    {
        for (int i = 0; i < _allCubes.Length; i++)
        {
            _allCubes[i].CubeWasGone += MarkCubeAsGone;
           
            var startPosition = _allCubes[i].transform.position;
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
            _cubesTargetPositions.Add(_allCubes[i].transform.position);
            _allCubes[i].transform.position = startPosition;
        }
    }

}