using System;
using Extensions;
using TetriminoMoving;
using Zenject;

namespace Player
{
    public class PlayerTetriminoMover: IInitializable, IDisposable
    {
        private readonly PlayerInputController _playerInputController;
        private readonly TetriminoMover _tetriminoMover;

        public PlayerTetriminoMover(PlayerInputController playerInputController, TetriminoMover tetriminoMover)
        {
            _playerInputController = playerInputController;
            _tetriminoMover = tetriminoMover;
        }
        
        public void Initialize()
        {
            _playerInputController.ActionButtonUp += OnPlayerButtonUp;
            _playerInputController.ActionButtonHold += OnPlayerButtonUp;
        }

        private void OnPlayerButtonUp(PlayerActionButton playerActionButton)
        {
            if (playerActionButton.ActionType.IsMoveAction())
            {
                MoveTetrimino(playerActionButton.ActionType);
            }

            if (playerActionButton.ActionType == ActionType.Drop)
            {
                DropTetrimino();
            }
        }

        private void DropTetrimino()
        {
            _tetriminoMover.DropTetrimino();
        }

        private void MoveTetrimino(ActionType playerAction)
        {
            _tetriminoMover.MoveTetrimino(playerAction.TransformInputToDirection());
        }

        public void Dispose()
        {
            _playerInputController.ActionButtonUp -= OnPlayerButtonUp;
        }
    }
}