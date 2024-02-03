using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class LevelConverter : MonoBehaviour
    {
        [SerializeField] private Grid _grid;
        [SerializeField] private GameObject _level;

        private Dictionary<int, Vector3> _cubesCellPositions = new Dictionary<int, Vector3>();
        private bool[,,] _levelInArray;
        private CubeMover[] _cubes;

        private Transform _idNextCube;

        private void Awake()
        {
            _levelInArray = new bool[2, 4, 2];
            _cubes = _level.transform.GetComponentsInChildren<CubeMover>();

            for (var i = 0; i < 2; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    for (var k = 0; k < 2; k++)
                    {
                        var cubeIndex = i * 4 * 2 + j * 2 + k;

                        var currentCubePosition = _cubes[cubeIndex].gameObject.transform.position;
                        var cellPosition = _grid.WorldToCell(currentCubePosition);

                        Debug.Log("count " + cubeIndex);
                        Debug.Log("cellPosition " + cellPosition);
                        Debug.Log("id " + _cubes[cubeIndex].transform.GetInstanceID());

                        _levelInArray[i, j, k] = true;
                        _cubesCellPositions.Add(_cubes[cubeIndex].transform.GetInstanceID(), cellPosition);
                    }
                }
            }
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, 50f)) return;
                
            CheckIfWayIsFree(hit.transform.GetInstanceID(), DetermineDirection(hit.transform));
        }

        private void CheckIfWayIsFree(int id, Vector3 direction)
        {
            var cellPosition = _cubesCellPositions[id];
            var nextCell = cellPosition + direction;

            if (nextCell.x < 0 || nextCell.x >= _levelInArray.GetLength(0) ||
                nextCell.y < 0 || nextCell.y >= _levelInArray.GetLength(1) ||
                nextCell.z < 0 || nextCell.z >= _levelInArray.GetLength(2))
            {
                _levelInArray[(int)cellPosition.x, (int)cellPosition.y, (int)cellPosition.z] = false;
                Debug.Log("Tthe way is freeeeee! goooooooooo (out of range)");
                return;
            }
            
            if (_levelInArray[(int)nextCell.x, (int)nextCell.y, (int)nextCell.z])
            {
                Debug.Log("cel " + nextCell + "is busy");
            }

            else
            {
                _levelInArray[(int)cellPosition.x, (int)cellPosition.y, (int)cellPosition.z] = false;
                Debug.Log("the way is freeeeee! goooooooooo");
            }

        }

        private Vector3 DetermineDirection(Transform transform)
        {
            var right = new Vector3(0, 180, 90);
            var left = new Vector3(0, 180, -90 + 360);
            var up = new Vector3(0, 0, 0);
            var down = new Vector3(-180 + 360, 0, 0);
            var forward = new Vector3(90, 0, 0);
            var back = new Vector3(-90 + 360, 0, 0);

            var eulerAngles = transform.localRotation.eulerAngles;

            switch (eulerAngles)
            {
                case var _ when eulerAngles == right:
                    return Vector3.right;
                case var _ when eulerAngles == left:
                    return Vector3.left;
                case var _ when eulerAngles == up:
                    return Vector3.up;
                case var _ when eulerAngles == down:
                    return Vector3.down;
                case var _ when eulerAngles == forward:
                    return Vector3.forward;
                case var _ when eulerAngles == back:
                    return Vector3.back;
                default:
                    Debug.Log("Углы не правильны");
                    return Vector3.zero;
            }
        }
    }
}