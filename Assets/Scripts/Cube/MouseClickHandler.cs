using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class MouseClickHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private bool IsCanClick = true;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        if (IsCanClick)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, 50f)) return;

            if (hit.transform.CompareTag("Cube"))
            {
                var cubeMover = hit.transform.GetComponent<CubeMover>();
                cubeMover.TryMove();
            }
        }
    }

    public void ClickEnabled()
    {
        IsCanClick = !IsCanClick;
    }
    

}