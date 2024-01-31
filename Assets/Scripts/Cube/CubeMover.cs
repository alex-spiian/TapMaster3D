using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
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
    
    private void Start()
    {
        // Получаем компонент AudioSource на текущем объекте
        _audioSource = GetComponents<AudioSource>();

        // Устанавливаем звук для AudioSource
        if(_audioSource!=null&&_audioSource.Length>1)
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
        if (!IsWayFree())
        {
            StartCoroutine(MoveToObstacle(GetRaycastHit()));
            return;
        }

        _isMoving = true;
        _initialPosition = transform.position;
        
        CubeWasGone?.Invoke();
    }
    
    
    private IEnumerator MoveToObstacle(RaycastHit [] raycastHit)
    {
        StartCoroutine(CheckObstacleAndMove(raycastHit));
        
        _initialPosition = transform.position;
        if (!raycastHit.Any())
        {
            yield break;
        }
        
        var target = raycastHit.First().collider.transform;

        var halfCubeSize = raycastHit.First().collider.bounds.size / 3f;

        while (target != null && Vector3.Distance(transform.position, target.position) > halfCubeSize.magnitude)
        {
            transform.Translate(Vector3.up * (_speed * Time.deltaTime));
            yield return null;
        }

        StartCoroutine(ShakeObstacles(raycastHit));
        StartCoroutine(MoveBack());

    }

    private IEnumerator ShakeObstacles(RaycastHit[] raycastHit)
    {
        var obstacles = raycastHit;
        for (var i = 0; i < obstacles.Length; i++)
        {
            if (obstacles[i].collider != null && obstacles[i].collider.transform != null)
            obstacles[i].collider.transform.DOPunchScale(new Vector3(0,0,0.5f),0.2f);
            _audioSource[1].Play();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator MoveBack()
    {
        while (Vector3.Distance(transform.position, _initialPosition) > 0.1f)
        {
            transform.Translate(-Vector3.up * (_speed * Time.deltaTime));
            yield return null;
        }

        transform.position = _initialPosition;
    }


    private bool IsWayFree()
    {
        var hits = GetRaycastHit();
        
        if (hits.Length > 0)
        {
            return false;
        }
        return true;
    }


    private RaycastHit [] GetRaycastHit()
    {
        Ray ray = new Ray(transform.position, transform.up);
        var hits = Physics.RaycastAll(ray, 5f);

        return hits;
    }
    
    private IEnumerator CheckObstacleAndMove(RaycastHit[] hits)
    {
        var lastPosition = hits.First().transform.position;
        
        yield return new WaitForSeconds(0.1f);

        if (Vector3.Distance(lastPosition, hits.First().transform.position) < 0.01f)
        {
            yield break;
        }
        
        Debug.Log("YEEEEHUUUUUUU");
        CubeWasGone?.Invoke();
    }
    
    
}