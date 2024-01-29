using UnityEngine;

public class MouseClickHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit, 50f)) return;

        if (hit.transform.tag == "Cube")
        {
            var cubeMover = hit.transform.GetComponent<CubeMover>();
            cubeMover.TryMove();
        }
    }
}