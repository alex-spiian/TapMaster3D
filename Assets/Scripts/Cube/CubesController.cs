using System;
using UnityEngine;

namespace Cube
{
    public class CubesController : MonoBehaviour
    {
        public event Action LastCubeWasGone;

        [SerializeField] private LevelsSpawner _levelsSpawner;
        [SerializeField] private BlackHole _blackHole;
        public int CountCubsInTotal { get; private set; }
        private int _countGoneCubes;

        private void Awake()
        {
            CountCubsInTotal = _levelsSpawner.CubesCount;
            _blackHole.BlackHoleWasClosed += MarkCubesAsGone;
            Debug.Log("cubes count = " + CountCubsInTotal);
        }

        public CubesController(int countCubsInTotal)
        {
            CountCubsInTotal = countCubsInTotal;
        }

        public void MarkCubesAsGone(int count)
        {
            CountCubsInTotal = _levelsSpawner.CubesCount;
            _countGoneCubes += count;
            
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

        private void OnDestroy()
        {
            _blackHole.BlackHoleWasClosed -= MarkCubesAsGone;
        }
    }
}