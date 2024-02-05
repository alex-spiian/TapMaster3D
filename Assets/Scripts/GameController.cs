using Cube;
using DefaultNamespace.Player;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private ScreensController.ScreensController _screensController;
        [SerializeField]
        private CubesController _cubesController;

        [SerializeField] private MouseClickHandler _mouseClickHandler;
        [SerializeField] private MovesCounter _movesCounter;

        [SerializeField] private WonMoneyController _wonMoneyController;
        [SerializeField] private WonMoneyControllerView _wonMoneyControllerView;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private WalletView _walletView;
        
        private void Awake()
        {
            _cubesController.LastCubeWasGone += _screensController.ShowVictoryScreen;
            _screensController.VictoryScreenLoaded += _wonMoneyControllerView.ShowCountingCubes;
            _cubesController.LastCubeWasGone += _wonMoneyController.CalculateWonAmountMoney;
            _wonMoneyController.WinningMoneyCalculated += _wonMoneyControllerView.SetWonAmountMoney;
            _wonMoneyController.WinningMoneyCalculated += _wallet.UpdateAmountMoney;
            _wallet.AmountMoneyUpdated += _walletView.SetAmountMoney;

            _mouseClickHandler.CubeWasTaped += _movesCounter.SpendOneMove;
            _movesCounter.AllMovesWasSpent += _screensController.ShowDefeatScreen;
        }

        private void OnDestroy()
        {
            _cubesController.LastCubeWasGone += _screensController.ShowVictoryScreen;
            _screensController.VictoryScreenLoaded -= _wonMoneyControllerView.ShowCountingCubes;
            _cubesController.LastCubeWasGone -= _wonMoneyController.CalculateWonAmountMoney;
            _wonMoneyController.WinningMoneyCalculated -= _wonMoneyControllerView.SetWonAmountMoney;
            _wonMoneyController.WinningMoneyCalculated -= _wallet.UpdateAmountMoney;
            _wallet.AmountMoneyUpdated += _walletView.SetAmountMoney;
            
            _mouseClickHandler.CubeWasTaped -= _movesCounter.SpendOneMove;
            _movesCounter.AllMovesWasSpent -= _screensController.ShowDefeatScreen;
        }
    }
}