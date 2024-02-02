using System;
using System.Collections;
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
        if (!IsWayFree())
        {
            StartCoroutine(MoveToObstacle(GetRaycastHit()));
            return;
        }

        _isMoving = true;
        _initialPosition = transform.position;
        
        CubeWasGone?.Invoke();
        _soundsManager.PlayClick();
    }
    
    
    private IEnumerator MoveToObstacle(RaycastHit [] raycastHit)
    {
       
        raycastHit = raycastHit.OrderBy(hit => Vector3.Distance(transform.position, hit.point)).ToArray();
        StartCoroutine(CheckObstacleAndMove(raycastHit));
        _initialPosition = transform.position;
        
        var target = raycastHit.First().collider.transform;
        var halfCubeSize = raycastHit.First().collider.bounds.size / 2f;
        
        while (Vector3.Distance(transform.position, target.position) > halfCubeSize.magnitude)
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
            
            obstacles[i].collider.transform.DOPunchScale(new Vector3(0.5f,0.5f,0.5f),0.2f);
            _soundsManager.PlayCollision();

            if (obstacles[i].collider.CompareTag("Cube"))
            {
                obstacles[i].collider.transform.DOScale(Vector3.one, 0.2f);
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
        var hits = GetRaycastHit();

        if (hits.Length > 0)
        {
            if (!hits.First().transform.CompareTag("Cube"))
            {
                return true;
            }
            
            return false;
        }
        return true;
    }


    private RaycastHit [] GetRaycastHit()
    {
        Ray ray = new Ray(transform.position, transform.up);
        var hits = Physics.RaycastAll(ray, 10f);

        return hits;
    }
    
    private IEnumerator CheckObstacleAndMove(RaycastHit[] hits)
    {
        var lastPosition = hits.First().transform.position;
        
        yield return new WaitForSeconds(0.1f);

        // попробовать сделать рекурсивную проверку так как если впереди летит 2+ куба то проблема остается
        if (Vector3.Distance(lastPosition, hits.First().transform.position) < 0.01f || hits.Length > 1)
        {
            yield break;
        }
        CubeWasGone?.Invoke();
        _soundsManager.PlayClick();
    }


}