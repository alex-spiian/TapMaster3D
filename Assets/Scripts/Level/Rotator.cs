using System;
using System.Collections;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private float _sensitivity;

    private bool _canRotate;

    private void Awake()
    {
        StartCoroutine(DelayBeforeStart());
    }

    private void Update()
    {
        if (!Input.GetMouseButton(0) || !_canRotate) return;
        
        var rotationX = Input.GetAxis("Mouse X") * _sensitivity;
        var rotationY = Input.GetAxis("Mouse Y") * _sensitivity;
            
        transform.RotateAround(transform.position, Vector3.up, -rotationX);
        transform.RotateAround(transform.position, Vector3.right, rotationY);
    }

    private IEnumerator DelayBeforeStart()
    {
        yield return new WaitForSeconds(2.5f);
        _canRotate = true;
    }
}
