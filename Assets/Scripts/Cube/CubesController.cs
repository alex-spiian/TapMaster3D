using System;
using UnityEngine;

namespace Cube
{
    public class CubesController : MonoBehaviour
    {
        public event Action LastCubeWasGone;

        [SerializeField] private LevelsSpawner _levelsSpawner;
        public int CountCubsInTotal { get; private set; }
        private int _countGoneCubes;

        private void Awake()
        {
            CountCubsInTotal = _levelsSpawner.CubesCount;
            Debug.Log("cubes count = " + CountCubsInTotal);
        }

        public CubesController(int countCubsInTotal)
        {
            CountCubsInTotal = countCubsInTotal;
        }
        
        public void MarkCubeAsGone()
        {
            CountCubsInTotal = _levelsSpawner.CubesCount;
            _countGoneCubes++;
            
           Debug.Log("cubes in total " + CountCubsInTotal);
           Debug.Log("cube # " + _countGoneCubes +" was gone");
            
            if (_countGoneCubes == CountCubsInTotal)
            {
                LastCubeWasGone?.Invoke();
            }
        }

        public void Reset()
        {
            _countGoneCubes = 0;
        }

    }
}