using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Level;
using UnityEngine;
using Random = UnityEngine.Random;

public class FiguresSpawner : MonoBehaviour
{
    public event Action LevelWasCompleted;
    
    [SerializeField]
    private FiguresSpawnerConfig _figuresSpawnerConfig;
    
    private CubeMover[] _childrenTransforms;
    private readonly List<Vector3> _cubesTargetPositions = new();
    private int _counterOfGoneCubes;

    private LevelsController _levelsController;
    
    
    private  void Awake()
    {
        // TODO: temp code
        _levelsController = FindAnyObjectByType<LevelsController>();
        
        LevelWasCompleted += _levelsController.LoadNextLevel;
        
        _childrenTransforms = GetComponentsInChildren<CubeMover>();

        transform.position = new Vector3(0, 0, 6);
        
        SetStartPositionForChildren();
        StartCoroutine(MoveCubesInRow());
    }

    private void MarkCubeAsGone()
    {
        _counterOfGoneCubes++;

        if (_counterOfGoneCubes == _childrenTransforms.Length)
        {
            LevelWasCompleted?.Invoke();
        }
    }
    

    private IEnumerator MoveCubesInRow()
    {
        for (int i = 0; i < _childrenTransforms.Length; i++)
        {
            _childrenTransforms[i].transform.DOMove(_cubesTargetPositions[i], 1);
            var minDelay = _figuresSpawnerConfig.MinDelayBetweenDrops;
            var maxDelay = _figuresSpawnerConfig.MaxDelayBetweenDrops;
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        }
    }

    private void SetStartPositionForChildren()
    {
        for (int i = 0; i < _childrenTransforms.Length; i++)
        {
            _childrenTransforms[i].CubeWasGone += MarkCubeAsGone;
           
            var startPosition = _childrenTransforms[i].transform.position;
            if (i%2==0) 
            {
                startPosition.y += _figuresSpawnerConfig.YValueToBeOutOfCamera;
            }
            else if (i%3==0)
            {
                startPosition.x += _figuresSpawnerConfig.YValueToBeOutOfCamera;
            }
            else if(i%3==1)
            {
                startPosition.x -= _figuresSpawnerConfig.YValueToBeOutOfCamera;
            }
            else
            {
                startPosition.y -= _figuresSpawnerConfig.YValueToBeOutOfCamera;
            }
            _cubesTargetPositions.Add(_childrenTransforms[i].transform.position);
            _childrenTransforms[i].transform.position = startPosition;
        }
    }

    private void OnDestroy()
    {
        LevelWasCompleted -= _levelsController.LoadNextLevel;

    }
}