using System;
using DefaultNamespace.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cube
{
    public class CubesController : MonoBehaviour
    {
        //public event Action LastCubeWasGone;

        [SerializeField] private FinishLevelHandler _finishLevelHandler;
        [SerializeField] private LevelsSpawner _levelsSpawner;
        [SerializeField] private BlackHoleController blackHoleController;
        public int CountCubsInTotal { get; private set; }
        private int _countGoneCubes;

        public void Initialize()
        {
            CountCubsInTotal = _levelsSpawner.CubesCount;
            blackHoleController.BlackHoleWasClosed += MarkCubesAsGone;
            
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
                _finishLevelHandler.OnAllCubesAreGone();
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