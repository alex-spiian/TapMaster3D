using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.SoundsManager;
using DG.Tweening;
using UnityEngine;

public class CubeMover : MonoBehaviour
{
    public event Action CubeWasGone;

    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _speed;

    private SoundsManager _soundsManager;
    private Vector3 _initialPosition;
    private bool _isMoving;
    private RaycastHit _hit;

    private List<Transform> _neighbours = new List<Transform>();

    private void Awake()
    {
        _soundsManager = Container.Instance.SoundsManager;
        _initialPosition = transform.position;

    }

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.up);
        Physics.Raycast(ray, out var hit, 50f);
        Debug.DrawRay(transform.position, transform.up, Color.blue);
    }

    public void TryMove()
    {
        if (IsWayFree())
        {
            var globalDirection = transform.TransformDirection(_direction);
            transform.DOMove(globalDirection * 100, 8);

        }
        else
        {
            FindNeighbour(_hit.transform);
            ShakeObstacles();

            for (int i = 0; i < _neighbours.Count; i++)
            {
                Debug.Log(_neighbours[i].transform.GetInstanceID());
            }
            MoveToObstacle();
        }

        _isMoving = true;
    }


    private void MoveToObstacle()
    {
        _initialPosition = transform.localPosition;
        var sequence = DOTween.Sequence();
        Vector3 targetPosition = _hit.transform.localPosition -
                                 (_hit.transform.localPosition - transform.localPosition).normalized * 0.9f;
        sequence.Append(transform.DOLocalMove(targetPosition, 0.5f)).Complete();
        sequence.Append(transform.DOLocalMove(_initialPosition, 0.5f));
        sequence.Play();
    }

    private bool IsWayFree()
    {
        _hit = GetRaycastHit(transform, 10f);
        
        if (_hit.collider != null)
        {
            if (_hit.transform.CompareTag("Cube"))
            {
                return false;
            }
        }
        return true;
    }
    
    private void ShakeObstacles()
    {
        for (var i = 0; i < _neighbours.Count; i++)
        {
            _neighbours[i].transform.DOPunchScale(new Vector3(0.5f,0.5f,0.5f),0.2f);
            _soundsManager.PlayCollision();

            if (_neighbours[i].transform.CompareTag("Cube"))
            {
                _neighbours[i].transform.DOScale(Vector3.one, 0.2f);
            }
        }
        _neighbours.Clear();
    }

    private RaycastHit GetRaycastHit(Transform cubeTransform, float rayLenght)
    {
        Ray ray = new Ray(cubeTransform.position, transform.up);
        Physics.Raycast(ray, out var hit, rayLenght);
        return hit;
    }

    private void FindNeighbour(Transform cubeTransform)
    {
        var frontCube = GetRaycastHit(cubeTransform, 0.53f);
        if (frontCube.transform != null && frontCube.transform.CompareTag("Cube"))
        {
            _neighbours.Add(frontCube.transform);
            var nextCubeTransform = GetRaycastHit(frontCube.transform, 0.53f).transform;
            if (nextCubeTransform != null)
            {
                FindNeighbour(nextCubeTransform);
            }
        }
    }

    





}