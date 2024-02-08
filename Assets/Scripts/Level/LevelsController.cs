using Cube;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level
{
    public class LevelsController : MonoBehaviour
    {
        [SerializeField] private LevelsSpawner _levelsSpawner;
        [SerializeField] private LevelsSwitcher _levelsSwitcher;
        [SerializeField] private LevelView _levelView;
        [SerializeField] private LevelsScreenController _levelsScreenController;
        [SerializeField] private CubesController _cubesController;
        [SerializeField] private ScreensController.ScreensController _screensController;

        public void Initialize()
        {
            _levelsSpawner.SpawnLevel(_levelsSwitcher.CurrentLevelIndex);
            _levelView.UpdateCurrentLevelView();
            _levelsScreenController.Initialize();
            
            _levelsSwitcher.LevelWasChanged += _levelView.UpdateCurrentLevelView;
            _levelsSwitcher.LevelWasChanged += _levelsScreenController.UpdateLevelsScreen;
            _levelsSwitcher.LevelWasChanged += _cubesController.Reset;
            _levelsSwitcher.LevelWasRestarted += _cubesController.Reset;

            _levelsSwitcher.GameWasStartedFromBeginning += _levelView.UpdateCurrentLevelView;
            _levelsSwitcher.GameWasStartedFromBeginning += _levelsScreenController.ResetLevels;
            _levelsSwitcher.GameWasStartedFromBeginning += _levelsScreenController.UpdateLevelsScreen;
            _levelsSwitcher.GameWasStartedFromBeginning += _cubesController.Reset;
            
            _levelsSwitcher.GameWasCompleted += _screensController.ShowGameCompletedScreen;

        }

        private void OnDestroy()
        {
            _levelsSwitcher.LevelWasChanged -= _levelView.UpdateCurrentLevelView;
            _levelsSwitcher.LevelWasChanged -= _levelsScreenController.UpdateLevelsScreen;
            _levelsSwitcher.LevelWasChanged -= _cubesController.Reset;
            _levelsSwitcher.LevelWasRestarted -= _cubesController.Reset;

            _levelsSwitcher.GameWasStartedFromBeginning -= _levelView.UpdateCurrentLevelView;
            _levelsSwitcher.GameWasStartedFromBeginning -= _levelsScreenController.ResetLevels;
            _levelsSwitcher.GameWasStartedFromBeginning -= _levelsScreenController.UpdateLevelsScreen;
            _levelsSwitcher.GameWasStartedFromBeginning -= _cubesController.Reset;
            
            _levelsSwitcher.GameWasCompleted -= _screensController.ShowGameCompletedScreen;
        }
    }
}