using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class MouseClickHandler : MonoBehaviour
{
    public event Action CubeWasTaped;
    
    [SerializeField] private Camera _camera;

    private bool CanClick = true;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        if (CanClick)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, 50f)) return;

            if (hit.transform.CompareTag("Cube"))
            {
                var cubeMover = hit.transform.GetComponent<CubeMover>();
                if (cubeMover.IsMoving) return;
                
                cubeMover.TryMove();
                CubeWasTaped?.Invoke();
            }
        }
    }

    public void ClickEnabled(bool onOff)
    {
        CanClick = onOff;
    }
    

}