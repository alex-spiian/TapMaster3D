using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.SoundsManager;
using DG.Tweening;
using Unity.VisualScripting;
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
    private List<RaycastHit> _hitsShakeAnimation = new();
    private int _countRaycast;

    private void Awake()
    {
        _soundsManager = Container.Instance.SoundsManager;
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
            MoveToObstacle();
        }

        _isMoving = true;
        _initialPosition = transform.position;
    }


    private void MoveToObstacle()
    {
        _initialPosition = transform.localPosition;
        var sequence = DOTween.Sequence();
        Vector3 targetPosition = _hit.transform.localPosition -
                                 (_hit.transform.localPosition - transform.localPosition).normalized * 0.9f;
        sequence.Append(transform.DOLocalMove(targetPosition, 0.5f)).Complete();
        StartCoroutine( ShakeAnimation());
        sequence.Append(transform.DOLocalMove(_initialPosition, 0.5f));
        sequence.Play();
    }

    private IEnumerator ShakeAnimation()
    {
        for (var i = 0; i < _hitsShakeAnimation.Count; i++)
        {
            _hitsShakeAnimation[i].collider.transform.DOPunchScale(new Vector3(0.5f,0.5f,0.5f),0.2f);
            _hitsShakeAnimation[i].collider.transform.DOScale(Vector3.one, 0.2f);
            yield return new WaitForSeconds(0.1f);
        }

        _countRaycast = 0;
    }

    private bool IsWayFree()
    {
        CubeRaycastOnClick();
        CubeRaycastOnClickForShakeAnimation();
        if (_hit.collider != null)
        {
            if (_hit.transform.CompareTag("Cube"))
            {
                return false;
            }
        }

        return true;
    }


    private void CubeRaycastOnClick()
    {
        Ray ray = new Ray(transform.position, transform.up);
        Physics.Raycast(ray, out _hit, 10f);
    }

   
    private void CubeRaycastOnClickForShakeAnimation()
    {
        if (_hitsShakeAnimation.Count!=0)
        {
            _hitsShakeAnimation.Clear();
        }
        
        Ray ray = new Ray(transform.position, transform.up);
        
        RecursiveObstacleRaycast(ray);
    }

    private void RecursiveObstacleRaycast(Ray ray)
    {
        var maxDistance = 10;
        if (_countRaycast>0)
        {
            maxDistance = 1;
        }
        if (Physics.Raycast(ray, out var  hit, maxDistance))
        {
            _hitsShakeAnimation.Add(hit);
            if (hit.collider!=null)
            {
                Ray obstacleRay = new Ray(hit.transform.position, transform.up);
                _countRaycast++;
                RecursiveObstacleRaycast(obstacleRay);
            }
        }
    }
}