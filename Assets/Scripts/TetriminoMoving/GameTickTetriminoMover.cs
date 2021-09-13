using Game;
using TetriminoMoving.Data;
using Zenject;

namespace TetriminoMoving
{
    public class GameTickTetriminoMover : IInitializable
    {
        private readonly GameTicker _gameTicker;
        private readonly TetriminoMover _tetriminoMover;

        public GameTickTetriminoMover(GameTicker gameTicker, TetriminoMover tetriminoMover)
        {
            _gameTicker = gameTicker;
            _tetriminoMover = tetriminoMover;
        }

        public void Initialize()
        {
            _gameTicker.GameTicked += OnGameTicked;
        }

        private void OnGameTicked()
        {
            _tetriminoMover.MoveTetriminoInDirection(MoveDirection.Down);
        }
    }
}