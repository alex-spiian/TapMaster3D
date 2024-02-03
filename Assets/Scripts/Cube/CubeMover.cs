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
            //transform.Translate(_direction * (_speed * Time.deltaTime));
            
            var globalDirection = transform.TransformDirection(_direction);
            var tween = transform.DOMove(globalDirection * 100, 20);
            if (tween.IsComplete())
            {
                Destroy(gameObject);
            }

        }
    }

    public void TryMove()
    {
        if (!IsWayFree())
        {
            MoveToObstacle(GetRaycastHit());        
            return;
        }

        _isMoving = true;
        _initialPosition = transform.localPosition;
        
        CubeWasGone?.Invoke();
        _soundsManager.PlayClick();
        
        Debug.Log("Куб улетел в TryMove");
    }
    
    
    private void MoveToObstacle(RaycastHit [] raycastHit)
    {
        //StartCoroutine(CheckObstacleAndMove(raycastHit));
        _initialPosition = transform.localPosition;
        
        var target = raycastHit.First().transform;
        _initialPosition = transform.localPosition;
        
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOPunchPosition((target.localPosition - _initialPosition) / 2f, 0.2f));
        //sequence.Append(transform.DOLocalMove(_initialPosition, 0.2f));
        sequence.Play();
        
        StartCoroutine(ShakeObstacles(raycastHit));
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
                obstacles[i].transform.DOScale(Vector3.one, 0.2f);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator MoveBack()
    {
        Quaternion initialRotation = transform.parent.rotation;
        
        while (Vector3.Distance(transform.localPosition, _initialPosition) > 0.2f)
        {
            //transform.parent.rotation = initialRotation;
            transform.Translate(-Vector3.up * (_speed * Time.deltaTime));
            yield return null;
        }

        transform.localPosition = _initialPosition;
        //transform.parent.rotation = initialRotation;
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
        
        
        hits = hits.OrderBy(hit => Vector3.Distance(transform.localPosition, hit.point)).ToArray();

        return hits;
    }
    
    private IEnumerator CheckObstacleAndMove(RaycastHit[] hits)
    {
        var lastPosition = hits.First().transform.localPosition;
        
        yield return new WaitForSeconds(0.1f);

        // попробовать сделать рекурсивную проверку так как если впереди летит 2+ куба то проблема остается
        if (Vector3.Distance(lastPosition, hits.First().transform.localPosition) < 0.01f || hits.Length > 1)
        {
            yield break;
        }
        
        CubeWasGone?.Invoke();
        _soundsManager.PlayClick();
        Debug.Log("Куб улетел в CheckObstacleAndMove");

    }


}