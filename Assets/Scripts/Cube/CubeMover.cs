using System;
using System.Collections;
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
    private List<RaycastHit> _allHits = new();
    private List<RaycastHit> _hitsShakeAnimation = new();

    private void Awake()
    {
        _soundsManager = Container.Instance.SoundsManager;
    }

    private void Update()
    {
        if (_isMoving )
        {
            transform.Translate(_direction * (_speed * Time.deltaTime));
        }
    }

    public void TryMove()
    {
        RaycastHit();
        if (!IsWayFree())
        {
            StartCoroutine(MoveToObstacle());
            return;
        }

        _isMoving = true;
        _initialPosition = transform.position;
        
        CubeWasGone?.Invoke();
        _soundsManager.PlayClick();
    }
    
    
    private IEnumerator MoveToObstacle()
    {
       
         _allHits.OrderBy(hit => Vector3.Distance(transform.position, hit.point)).ToArray();
        StartCoroutine(CheckObstacleAndMove());
        _initialPosition = transform.position;
        
        var target = _allHits.First().collider.transform;
        var halfCubeSize = _allHits.First().collider.bounds.size / 2f;
        
        while (Vector3.Distance(transform.position, target.position) > halfCubeSize.magnitude)
        {
            transform.Translate(Vector3.up * (_speed * Time.deltaTime));
            yield return null;
        }
        
        StartCoroutine(ShakeObstacles());
        StartCoroutine(MoveBack());

    }

    private IEnumerator ShakeObstacles()
    {
        for (var i = 0; i < _hitsShakeAnimation.Count; i++)
        {
            _hitsShakeAnimation[i].collider.transform.DOPunchScale(new Vector3(0.5f,0.5f,0.5f),0.2f);
            _soundsManager.PlayCollision();

            if (_hitsShakeAnimation[i].collider.CompareTag("Cube"))
            {
                _hitsShakeAnimation[i].collider.transform.DOScale(Vector3.one, 0.2f);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator MoveBack()
    {
        Quaternion initialRotation = transform.parent.rotation;
        
        while (Vector3.Distance(transform.position, _initialPosition) > 0.2f)
        {
            transform.parent.rotation = initialRotation;

            transform.Translate(-Vector3.up * (_speed * Time.deltaTime));
            yield return null;
        }

        transform.position = _initialPosition;
        transform.parent.rotation = initialRotation;
    }


    private bool IsWayFree()
    {
        if (_allHits.Count > 0)
        {
            if (!_allHits.First().transform.CompareTag("Cube"))
            {
                return true;
            }
            
            return false;
        }
        return true;
    }


    private void RaycastHit()
    {
        if (_allHits.Count!=0)
        {
            _allHits.Clear();
        }

        if (_hitsShakeAnimation.Count!=0)
        {
            _hitsShakeAnimation.Clear();
        }
        Ray ray = new Ray(transform.position, transform.up);
        _allHits = Physics.RaycastAll(ray, 10f).ToList();
        _allHits.Sort((hit1, hit2) => hit1.transform.position.y.CompareTo(hit2.transform.position.y));
        for (var i = 0; i < _allHits.Count-1; i++)
        {
            var distanceBetweenCubes = Vector3.Distance(_allHits[i].transform.position,_allHits[i+1].transform.position);
            if (distanceBetweenCubes<1.1f)
            {
                _hitsShakeAnimation.Add(_allHits[i]);
            }

            if (i==_allHits.Count-2)
            {
                _hitsShakeAnimation.Add(_allHits[i+1]);
            }
            else if (distanceBetweenCubes>1.1f)
            {
                _hitsShakeAnimation.Add(_allHits[i]); 
                break;
            }
        }

        
    }
    
    private IEnumerator CheckObstacleAndMove()
    {
        var lastPosition = _allHits.First().transform.position;
        
        yield return new WaitForSeconds(0.1f);

        // попробовать сделать рекурсивную проверку так как если впереди летит 2+ куба то проблема остается
        if (Vector3.Distance(lastPosition, _allHits.First().transform.position) < 0.01f || _allHits.Count > 1)
        {
            yield break;
        }
        
        CubeWasGone?.Invoke();
        _soundsManager.PlayClick();
    }


}