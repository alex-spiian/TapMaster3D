using UnityEngine;

public class CubesSpawnerTemp : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    [SerializeField] private Transform _parent;
    [SerializeField] private GameObject[] _allPrefabs;
    private int _countCubes = 2;

    private void Awake()
    {
        for (int x = 0; x < _countCubes; x++)
        {
            for (int y = 0; y < _countCubes; y++)
            {
                for (int z = 0; z < _countCubes; z++)
                {
                    // if (x > 0 && x <= _countCubes - 2 && y > _countCubes - 2 && z >= 1 && z <= _countCubes - 2)
                    // {
                    //     continue;
                    // }

                    // if (x >= 1 && x <= _countCubes - 2 && y < 1 && z >= 1 && z <= _countCubes - 2)
                    // {
                    //     continue;
                    // }

                    // if (x < 1 && y >= 1 && y <= _countCubes - 2 && z >= 1 && z <= _countCubes - 2)
                    // {
                    //     continue;
                    // }

                    // if (x > _countCubes - 2 && y >= 1 && y <= _countCubes - 2 && z >= 1 && z <= _countCubes - 2)
                    // {
                    //     continue;
                    // }

                    // if (x >= 1 && x <= _countCubes - 2 && y >= 1 && y <= _countCubes - 2 && z < 1)
                    // {
                    //     continue;
                    // }

                    // if (x >= 1 && x <= _countCubes - 2 && y >= 1 && y <= _countCubes - 2 && z > _countCubes - 2)
                    // {
                    //     continue;
                    // }

                    var randomIndexCube = Random.Range(0, _allPrefabs.Length);
                    var cube = Instantiate(_allPrefabs[randomIndexCube], _parent);
                    cube.transform.position = _grid.CellToWorld(new Vector3Int(x, y, z));
                }
            }
        }
    }
}