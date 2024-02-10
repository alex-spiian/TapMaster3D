using System;
using Cube;
using DefaultNamespace.Player;
using Level;
using UnityEngine;
using Wallet;

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private ScreensController.ScreensController _screensController;
        [SerializeField] private CubesController _cubesController;
        [SerializeField] private MouseClickHandler _mouseClickHandler;
        [SerializeField] private MovesCounter _movesCounter;
        [SerializeField] private WonMoneyController _wonMoneyController;
        [SerializeField] private WonMoneyControllerView _wonMoneyControllerView;
        [SerializeField] private Wallet.Wallet _wallet;
        [SerializeField] private WalletView _walletView;
        [SerializeField] private LevelsController  _levelsController;
        [SerializeField] private SoundsManager.SoundsManager  _soundsManager;
        [SerializeField] private FinishLevelHandler _finishLevelHandler;
        
        private void Awake()
        {
            _finishLevelHandler.LevelWasCompleted += _screensController.ShowVictoryScreen;
            _finishLevelHandler.LevelWasCompleted += _soundsManager.PlayVictory;
            _finishLevelHandler.LevelWasCompleted += _wonMoneyController.CalculateWonAmountMoney;

            _wonMoneyController.WinningMoneyCalculated += _wonMoneyControllerView.SetWonAmountMoney;
            _wonMoneyController.WinningMoneyCalculated += _wallet.AddMoney;

            _mouseClickHandler.CubeWasTapped += _movesCounter.SpendOneMove;
            _mouseClickHandler.CubeWasTapped += _soundsManager.PlayClick;
            
            _finishLevelHandler.LevelWasFailed += _screensController.ShowDefeatScreen;
            _finishLevelHandler.LevelWasFailed += _soundsManager.PlayDefeat;
            
            _screensController.VictoryScreenLoaded += _wonMoneyControllerView.ShowCountingCubes;
            _wallet.AmountMoneyUpdated += _walletView.SetAmountMoney;
        }
        
        public void StartGame()
        {
            _levelsController.Initialize();
            _cubesController.Initialize();
        }

        private void OnDestroy()
        {
            _finishLevelHandler.LevelWasCompleted -= _screensController.ShowVictoryScreen;
            _finishLevelHandler.LevelWasCompleted -= _soundsManager.PlayVictory;
            _finishLevelHandler.LevelWasCompleted -= _wonMoneyController.CalculateWonAmountMoney;

            _wonMoneyController.WinningMoneyCalculated -= _wonMoneyControllerView.SetWonAmountMoney;
            _wonMoneyController.WinningMoneyCalculated -= _wallet.AddMoney;

            _mouseClickHandler.CubeWasTapped -= _movesCounter.SpendOneMove;
            _mouseClickHandler.CubeWasTapped -= _soundsManager.PlayClick;
            
            _finishLevelHandler.LevelWasFailed -= _screensController.ShowDefeatScreen;
            _finishLevelHandler.LevelWasFailed -= _soundsManager.PlayDefeat;
            
            _screensController.VictoryScreenLoaded -= _wonMoneyControllerView.ShowCountingCubes;
            _wallet.AmountMoneyUpdated -= _walletView.SetAmountMoney;
        }
        
    }
}