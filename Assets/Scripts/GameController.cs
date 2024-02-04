using Cube;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private ScreensController.ScreensController _screensController;
        [SerializeField]
        private CubesController _cubesController;

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
        }

        private void OnDestroy()
        {
            _cubesController.LastCubeWasGone += _screensController.ShowVictoryScreen;
            _screensController.VictoryScreenLoaded -= _wonMoneyControllerView.ShowCountingCubes;
            _cubesController.LastCubeWasGone -= _wonMoneyController.CalculateWonAmountMoney;
            _wonMoneyController.WinningMoneyCalculated -= _wonMoneyControllerView.SetWonAmountMoney;
            _wonMoneyController.WinningMoneyCalculated -= _wallet.UpdateAmountMoney;
            _wallet.AmountMoneyUpdated += _walletView.SetAmountMoney;
        }
    }
}