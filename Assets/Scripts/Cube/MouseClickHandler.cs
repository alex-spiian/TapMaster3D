using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class MouseClickHandler : MonoBehaviour
{
    public event Action CubeWasTapped;
    [SerializeField] private Camera _camera;

    private bool CanClick;
    private bool WasMouseButtonClicked;
    private Vector3 _initialMousePosition;

    private void Update()
    {
        Debug.Log(CanClick);
        
        if (!CanClick)
        {
            return;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            _initialMousePosition = Input.mousePosition;
        }
        
        if (!Input.GetMouseButtonUp(0)) return;
        if (Vector3.Distance(_initialMousePosition, Input.mousePosition) >= 10f) return;

        if (CanClick)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, 50f)) return;

            if (hit.transform.CompareTag("Cube"))
            {
                var cubeMover = hit.transform.GetComponent<CubeMover>();
                if (cubeMover.IsMoving) return;

                cubeMover.TryMove();
                CubeWasTapped?.Invoke();
                
            }
        }
    }

    public void ClickEnabled(bool onOff)
    {
        CanClick = onOff;
    }
    
    public void ClickEnabled2(bool onOff)
    {
        CanClick = onOff;
    }
    
    

}