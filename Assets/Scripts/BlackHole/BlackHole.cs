using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class BlackHole : MonoBehaviour
{
    public UnityEvent RayBlackHoleDisabled;
    
    [SerializeField] private Transform _levelPosition;
    private int _maxDistance = 100;

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
        var currentTime = 2f;
        while (currentTime>=0)
        {
            currentTime -= Time.deltaTime;
            if (Physics.Raycast(ray, out var hit, _maxDistance))
            {
             
                hit.rigidbody.isKinematic = false;
                hit.collider.gameObject.transform.SetParent(null);
            }

            yield return null;
        }

        yield return new WaitForSeconds(3);
        RayBlackHoleDisabled?.Invoke();
    }
 
    
}