using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class BlackHole : MonoBehaviour
{
    public event Action<int> BlackHoleWasClosed;
    [SerializeField] private Transform _levelPosition;
    private int _maxDistance = 100;
    private Vector3 _initialScale;
    private int _destroyedCubesCount;
    
    private void Awake()
    {
        _initialScale = transform.localScale;
    }

    public void OnEnableBlackHole()
    {
        var direction = _levelPosition.position - transform.position;
        var ray = new Ray(transform.position, direction);
        {
            StartCoroutine(OnEnableBlackHoleCoroutine(ray));
        }
    }

    private IEnumerator OnEnableBlackHoleCoroutine(Ray ray)
    {
        transform.localScale = _initialScale;
        
        var currentTime = 2f;
        while (currentTime>=0)
        {
            currentTime -= Time.deltaTime;
            if (Physics.Raycast(ray, out var hit, _maxDistance))
            {
             
                hit.rigidbody.isKinematic = false;
                hit.collider.gameObject.transform.SetParent(null);


                hit.collider.gameObject.transform.DOScale(Vector3.zero, 1f);
            }

            yield return null;
        }

        yield return new WaitForSeconds(2);
        
        CloseBlackHole();
    }

    private void CloseBlackHole()
    {
        transform.DOScale(Vector3.zero, 1f).OnComplete(() => transform.gameObject.SetActive(false));
        BlackHoleWasClosed?.Invoke(_destroyedCubesCount);
        _destroyedCubesCount = 0;
    }

    public void OnCubeWasDestroyed()
    {
        _destroyedCubesCount++;
    }
 
    
}