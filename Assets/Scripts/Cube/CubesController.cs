using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cube
{
    public class CubesController : MonoBehaviour
    {
        public event Action LastCubeWasGone;

        [SerializeField] private LevelsSpawner _levelsSpawner;
        [FormerlySerializedAs("_blackHole")] [SerializeField] private BlackHoleController blackHoleController;
        public int CountCubsInTotal { get; private set; }
        private int _countGoneCubes;

        public void Initialize()
        {
            CountCubsInTotal = _levelsSpawner.CubesCount;
            blackHoleController.BlackHoleWasClosed += MarkCubesAsGone;
            
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
            blackHoleController.BlackHoleWasClosed -= MarkCubesAsGone;
        }
    }
}