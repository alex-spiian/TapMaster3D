using Cube;
using UnityEngine;

namespace Level
{
    public class LevelsController : MonoBehaviour
    {
        [SerializeField] private LevelsSpawner _levelsSpawner;
        [SerializeField] private LevelsSwitcher _levelsSwitcher;
        [SerializeField] private LevelView levelView;
        [SerializeField] private LevelsScreenController _levelsScreenController;
        [SerializeField] private CubesController _cubesController;
        [SerializeField] private HintBooster _hintBooster;

        private void Awake()
        {
            _levelsSpawner.SpawnLevel(_levelsSwitcher.CurrentLevelIndex);
            levelView.UpdateCurrentLevelView();
            _levelsScreenController.Initialize();
            
            _levelsSwitcher.LevelWasChanged += levelView.UpdateCurrentLevelView;
            _levelsSwitcher.LevelWasChanged += _levelsScreenController.UpdateLevelsScreen;
            _levelsSwitcher.GameWasStartedFromBeginning += levelView.UpdateCurrentLevelView;
            _levelsSwitcher.GameWasStartedFromBeginning += _levelsScreenController.ResetLevels;
            _levelsSwitcher.GameWasStartedFromBeginning += _levelsScreenController.UpdateLevelsScreen;
            _levelsSwitcher.GameWasStartedFromBeginning += _cubesController.Reset;
            _levelsSwitcher.LevelWasRestarted += _cubesController.Reset;
            _levelsSwitcher.LevelWasRestarted += _hintBooster.LookForAvailableCubes;

        }

        private void OnDestroy()
        {
            _levelsSwitcher.LevelWasChanged -= levelView.UpdateCurrentLevelView;
            _levelsSwitcher.LevelWasChanged -= _levelsScreenController.UpdateLevelsScreen;
            _levelsSwitcher.GameWasStartedFromBeginning -= levelView.UpdateCurrentLevelView;
            _levelsSwitcher.GameWasStartedFromBeginning -= _levelsScreenController.ResetLevels;
            _levelsSwitcher.GameWasStartedFromBeginning -= _levelsScreenController.UpdateLevelsScreen;
            _levelsSwitcher.GameWasStartedFromBeginning -= _cubesController.Reset;
            _levelsSwitcher.LevelWasRestarted -= _cubesController.Reset;
            _levelsSwitcher.LevelWasRestarted -= _hintBooster.LookForAvailableCubes;
        }
    }
}