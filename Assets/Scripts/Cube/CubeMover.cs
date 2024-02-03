using System;
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
        sequence.Append(transform.DOLocalMove(_initialPosition, 0.5f));
        sequence.Play();
    }

    private bool IsWayFree()
    {
        GetRaycastHit();
        if (_hit.collider != null)
        {
            if (_hit.transform.CompareTag("Cube"))
            {
                return false;
            }
        }

        return true;
    }

    private void GetRaycastHit()
    {
        Ray ray = new Ray(transform.position, transform.up);
        Physics.Raycast(ray, out _hit, 10f);
    }





}