using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.Player
{
    public class MovesCounter : MonoBehaviour
    {
        //public event Action AllMovesWasSpent;
        public int AvailableMoves { get; private set; }

        [SerializeField] private TextMeshProUGUI _movesCountLabel;
        [SerializeField] private FinishLevelHandler _finishLevelHandler;

        public void InitializeMovesCount(int moves)
        {
            AvailableMoves = moves + Mathf.RoundToInt(moves * 0.60f);
            UpdateMoves();
        }

        private void UpdateMoves()
        {
            _movesCountLabel.text = AvailableMoves.ToString();
        }

        public void SpendOneMove()
        { 
            AvailableMoves--;
            if (AvailableMoves < 0) return;
            UpdateMoves();
            
            if (AvailableMoves == 0)
            {
                _finishLevelHandler.TryFinishLevel();
            }
        }

    }
}