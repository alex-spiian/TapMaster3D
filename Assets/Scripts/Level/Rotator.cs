using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private float _sensitivity;

    private bool CanRotate = true;
    
    private void Update()
    {
        if (!Input.GetMouseButton(0)) return;

        if (CanRotate)
        {
            var rotationX = Input.GetAxis("Mouse X") * _sensitivity;
            var rotationY = Input.GetAxis("Mouse Y") * _sensitivity;
            
            transform.RotateAround(transform.position, Vector3.up, -rotationX);
            transform.RotateAround(transform.position, Vector3.right, rotationY);
        }
    }
    
    
    public void RotateEnabled()
    {
        CanRotate = !CanRotate;
    }

}
