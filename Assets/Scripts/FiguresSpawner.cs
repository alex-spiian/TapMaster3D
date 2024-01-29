using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FiguresSpawner : MonoBehaviour
{
    [SerializeField]
    private FiguresSpawnerConfig _figuresSpawnerConfig;

    private Transform[] _childrenTransforms;
    private readonly List<Transform> _cubesTransforms = new();
    private readonly List<Vector3> _cubesTargetPositions = new();
    private  void Awake()
    {
        _childrenTransforms = GetComponentsInChildren<Transform>();

        for (int i = 0; i < _childrenTransforms.Length; i++)
        {
            if (_childrenTransforms[i].tag == "Cube")
            {
                _cubesTransforms.Add(_childrenTransforms[i]);
                
                var startPosition = _childrenTransforms[i].position;
                startPosition.y += _figuresSpawnerConfig.YValueToBeOutOfCamera;
                
                _cubesTargetPositions.Add(_childrenTransforms[i].position);
                _childrenTransforms[i].position = startPosition;
            }
        }
        
        StartCoroutine(MoveCubesInRow());
    }

    private IEnumerator MoveCubesInRow()
    {
        for (int i = 0; i < _cubesTransforms.Count; i++)
        {
            StartCoroutine(MoveCubeSmoothly(i));
            
            var minDelay = _figuresSpawnerConfig.MinDelayBetweenDrops;
            var maxDelay = _figuresSpawnerConfig.MaxDelayBetweenDrops;
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        }
    }

    private IEnumerator MoveCubeSmoothly(int index)
    {
        var currentTime = 0f;
        var initialPosition = _cubesTransforms[index].position;
        var targetPosition = _cubesTargetPositions[index];

        while (currentTime < _figuresSpawnerConfig.CubeFallingDuration)
        {
            currentTime += Time.deltaTime;
            var progress = Mathf.Clamp01(currentTime / _figuresSpawnerConfig.CubeFallingDuration);

            var newPosition = Vector3.Lerp(initialPosition, targetPosition, progress);
            _cubesTransforms[index].position = newPosition;

            yield return null;
        }
    }
}