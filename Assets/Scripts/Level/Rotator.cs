using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private float _sensitivity;

    private void Update()
    {
        if (!Input.GetMouseButton(0)) return;
        
        var rotationX = Input.GetAxis("Mouse X") * _sensitivity;
        var rotationY = Input.GetAxis("Mouse Y") * _sensitivity;
            
        transform.RotateAround(transform.position, Vector3.up, -rotationX);
        transform.RotateAround(transform.position, Vector3.right, rotationY);
    }
    
}
