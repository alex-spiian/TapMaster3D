using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class FiguresSpawner : MonoBehaviour
{
    [SerializeField]
    private FiguresSpawnerConfig _figuresSpawnerConfig;

    private CubeMover[] _childrenTransforms;
    private readonly List<Transform> _cubesTransforms = new();
    private readonly List<Vector3> _cubesTargetPositions = new();
    private  void Awake()
    {
        _childrenTransforms = GetComponentsInChildren<CubeMover>();

        for (int i = 0; i < _childrenTransforms.Length; i++)
        {
            if (_childrenTransforms[i].tag == "Cube")
            {
                _cubesTransforms.Add(_childrenTransforms[i].transform);
                
                var startPosition = _childrenTransforms[i].transform.position;
                if (i%2==0) 
                {
                    startPosition.y += _figuresSpawnerConfig.YValueToBeOutOfCamera;
                }
                else
                {
                    startPosition.y -= _figuresSpawnerConfig.YValueToBeOutOfCamera;
                }
                _cubesTargetPositions.Add(_childrenTransforms[i].transform.position);
                _childrenTransforms[i].transform.position = startPosition;
            }
        }
        
        StartCoroutine(MoveCubesInRow());
    }

   

    private IEnumerator MoveCubesInRow()
    {
        for (int i = 0; i < _cubesTransforms.Count; i++)
        {
            _childrenTransforms[i].transform.DOMove(_cubesTargetPositions[i], 1);
            var minDelay = _figuresSpawnerConfig.MinDelayBetweenDrops;
            var maxDelay = _figuresSpawnerConfig.MaxDelayBetweenDrops;
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        }
    }
}