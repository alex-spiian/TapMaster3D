using System.Collections;
using System.Linq;
using UnityEngine;

public class MouseClickHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit, 50f)) return;

        if (hit.transform.CompareTag("Cube"))
        {
            var cubeMover = hit.transform.GetComponent<CubeMover>();
            StartCoroutine(CheckWayAndMove(cubeMover));
        }
    }
    
    private IEnumerator CheckWayAndMove(CubeMover cube)
    {
        var lastPosition = cube.transform.position;
        yield return new WaitForSeconds(0.02f);

        if (Vector3.Distance(lastPosition, cube.transform.position) < 0.01f)
        {
            cube.TryMove();
        }
        yield return null;
    }
}