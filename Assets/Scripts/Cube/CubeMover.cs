using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cube;
using DefaultNamespace.SoundsManager;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class CubeMover : MonoBehaviour
{
    public event Action CubeWasGone;

    [SerializeField] private Vector3 _direction;

    private SoundsManager _soundsManager;
    private Vector3 _initialPosition;
    private bool _isMoving;
    private RaycastHit _hit;
    private List<RaycastHit> _hitsShakeAnimation = new();
    private int _countRaycast;
    private CubesController _cubesController;

    private void Awake()
    {
        _soundsManager = Container.Instance.SoundsManager;
        _cubesController = Container.Instance.CubesController;

        CubeWasGone += _cubesController.MarkCubeAsGone;
    }

    public void TryMove()
    {
        if (IsWayFree())
        {
            var globalDirection = transform.TransformDirection(_direction);
            transform.DOMove(globalDirection * 100, 40);
            _isMoving = true;
            transform.SetParent(null);
            
            var moveDirection = transform.up;
            moveDirection.Normalize();
            
            var moveDuration = 5f;
            var distance = 5 * moveDuration;
            var endPosition = transform.position + moveDirection * distance;

            transform.DOMove(endPosition, moveDuration).OnComplete(() => Destroy(gameObject));
            CubeWasGone?.Invoke();

            foreach (Transform child in transform)
            { 
                if (child.CompareTag("Effect"))
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
        
        else
        {
            MoveToObstacle();
            _isMoving = false;
        }

        
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

       // var initialPosition = transform.localPosition;
       // var endPosition =
       //     new Vector3(_hit.transform.localPosition.x, _hit.transform.localPosition.y, _hit.transform.localPosition.z)
       //     - transform.up;
//
//
       // transform.DOLocalMove(endPosition, 0.2f).OnComplete(() => transform.DOLocalMove(initialPosition, 0.2f));


    }

    private IEnumerator ShakeAnimation()
    {
        for (var i = 0; i < _hitsShakeAnimation.Count; i++)
        {
            _soundsManager.PlayCollision();
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
            if (_hit.transform.GetComponent<CubeMover>()._isMoving)
            {
                return true;
            }

            return false;
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
        LookForNeighbours(ray);
    }

    private void LookForNeighbours(Ray ray)
    {
        var maxDistance = 10;
        if (_countRaycast>0)
        {
            maxDistance = 1;
        }
        
        if (Physics.Raycast(ray, out var  hit, maxDistance) && hit.collider!=null)
        {
            _hitsShakeAnimation.Add(hit);
            Ray obstacleRay = new Ray(hit.transform.position, transform.up);
            _countRaycast++;
            LookForNeighbours(obstacleRay);
        }
    }

    private void OnDestroy()
    {
        CubeWasGone -= _cubesController.MarkCubeAsGone;
    }
}