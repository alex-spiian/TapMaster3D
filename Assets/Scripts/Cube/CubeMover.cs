using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class CubeMover : MonoBehaviour
{
    public event Action CubeWasGone;
    
    [SerializeField] private Vector3 _direction; 
    [SerializeField] private float _speed;
    [SerializeField] private AudioClip _soundToPlay; // Звук для воспроизведения

    [SerializeField]private AudioSource[] _audioSource;
    private Vector3 _initialPosition;
    private bool _isMoving;
    private int _maxDistanceRay = 1;
    private List<RaycastHit> _hits = new();

    private void Start()
    {
        // Получаем компонент AudioSource на текущем объекте
        _audioSource = GetComponents<AudioSource>();

        // Устанавливаем звук для AudioSource
        if (_audioSource != null && _audioSource.Length > 1) ;
        _audioSource[1].clip = _soundToPlay;
    }
    private void Update()
    {
        if (_isMoving)
        {
            transform.Translate(_direction * (_speed * Time.deltaTime));
        }
    }

    public void TryMove()
    {
        RayCube();
        if (!IsWayFree())
        {
            StartCoroutine(MoveToObstacle());
            return;
        }

        _isMoving = true;
        _initialPosition = transform.position;
        
        CubeWasGone?.Invoke();
    }
    
    
    private IEnumerator MoveToObstacle()
    {
        _hits = _hits.OrderBy(hit => Vector3.Distance(transform.position, hit.point)).ToList();
        StartCoroutine(CheckObstacleAndMove());
        
        _initialPosition = transform.position;
        if (!_hits.Any())
        {
            yield break;
        }
        
        var target = _hits.First().collider.transform;

        var halfCubeSize = _hits.First().collider.bounds.size / 2.5f;
        
        
        while (target != null && Vector3.Distance(transform.position, target.position) > halfCubeSize.magnitude)
        {
            transform.Translate(Vector3.up * (_speed * Time.deltaTime));
            yield return null;
        }

        StartCoroutine(ShakeObstacles());
        StartCoroutine(MoveBack());

    }

    private IEnumerator ShakeObstacles()
    {
        for (var i = 0; i < _hits.Count; i++)
        {
            if (_hits[i].collider != null && _hits[i].collider.transform != null)
                _hits[i].collider.transform.DOPunchScale(new Vector3(0.5f,0.5f,0.5f),0.2f);
            _audioSource[1].Play();
            _hits[i].collider.transform.DOScale(Vector3.one, 0.2f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator MoveBack()
    {
        while (Vector3.Distance(transform.position, _initialPosition) > 0.2f)
        {
            transform.Translate(-Vector3.up * (_speed * Time.deltaTime));
            yield return null;
        }

        transform.position = _initialPosition;
    }


    private bool IsWayFree()
    {
        if (_hits.Count > 0)
        {
            return false;
        }
        return true;
    }


    private void RayCube()
    {
        if (_hits.Count!=0)
        {
            _hits.Clear();
        }
        Ray ray = new Ray(transform.position, transform.up);
        RayObstacle(ray);
    }

    private void RayObstacle(Ray ray)
    {
        if (Physics.Raycast(ray, out var hit, _maxDistanceRay))
        {
            _hits.Add(hit);
            if (hit.collider!=null)
            {
                Ray obstacleRay = new Ray(hit.transform.position, transform.up);
                RayObstacle(obstacleRay);
            }
        }
    }

    private IEnumerator CheckObstacleAndMove()
    {
        var lastPosition = _hits.First().transform.position;
        
        yield return new WaitForSeconds(0.1f);

        if (Vector3.Distance(lastPosition, _hits.First().transform.position) < 0.01f)
        {
            yield break;
        }
        
        Debug.Log("YEEEEHUUUUUUU");
        CubeWasGone?.Invoke();
    }
    
    
}