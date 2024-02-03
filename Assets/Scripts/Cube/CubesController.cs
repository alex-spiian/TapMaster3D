using System;
using UnityEngine;

namespace Cube
{
    public class CubesController : MonoBehaviour
    {
        public event Action LastCubeWasGone;

        [SerializeField] private LevelsSpawner _levelsSpawner;
        private int _countCubsInTotal;
        private int _countGoneCubes;

        private void Awake()
        {
            _countCubsInTotal = _levelsSpawner.CubesCount;
            
           Debug.Log("cubes count = " + _countCubsInTotal);
        }

        public CubesController(int countCubsInTotal)
        {
            _countCubsInTotal = countCubsInTotal;
        }
        
        public void MarkCubeAsGone()
        {
            _countCubsInTotal = _levelsSpawner.CubesCount;
            _countGoneCubes++;
            
           Debug.Log("cubes in total " + _countCubsInTotal);
           Debug.Log("cube # " + _countGoneCubes +" was gone");
            
            if (_countGoneCubes == _countCubsInTotal)
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