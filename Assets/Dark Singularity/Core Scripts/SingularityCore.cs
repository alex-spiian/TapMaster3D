using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class SingularityCore : MonoBehaviour
{
    //This script is responsible for what happens when the pullable objects reach the core
    //by default, the game objects are simply turned off
    //as this is much more performant than destroying the objects

    [SerializeField] private BlackHole _blackHole;
    private int _countCubesDestroy;
    
    private void Awake()
    {
        if(GetComponent<SphereCollider>()){
            GetComponent<SphereCollider>().isTrigger = true;
        }
    }
    
    
    void OnTriggerStay (Collider other) {
        if(other.GetComponent<SingularityPullable>())
        {
            
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            Destroy(other.gameObject);
            _countCubesDestroy++;
            Debug.Log(_countCubesDestroy);
            
            _blackHole.OnCubeWasDestroyed();
        }
    }

    private void OnEnable()
    {
        _countCubesDestroy = 0;
    }
    
}
