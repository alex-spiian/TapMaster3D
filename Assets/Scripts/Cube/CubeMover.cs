using System.Collections;
using UnityEngine;

public class CubeMover : MonoBehaviour
{
    [SerializeField] private Vector3 _direction; 
    [SerializeField] private float _speed;

    private Vector3 _initialPosition;
    private bool _isMoving;
    
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
    }
    
    
    private IEnumerator MoveToObstacle(RaycastHit raycastHit)
    {
        _initialPosition = transform.position;
        var target = raycastHit.collider.transform;
        var halfCubeSize = raycastHit.collider.bounds.size / 3f;

        while (Vector3.Distance(transform.position, target.position) > halfCubeSize.magnitude)
        {
            transform.Translate(Vector3.up * (_speed * Time.deltaTime));
            yield return null;
        }

        StartCoroutine(MoveBack());
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
        var hit = GetRaycastHit();
        
        if (hit.collider != null)
        {
            return false;
        }
        return true;
    }


    private RaycastHit GetRaycastHit()
    {
        Ray ray = new Ray(transform.position, transform.up);

        Physics.Raycast(ray, out var hit, 5f);

        return hit;
    }
    
}
