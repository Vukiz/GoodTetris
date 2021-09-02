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
            _playerInputController.ActionButtonHold += OnPlayerButtonHold;
        }

        private void OnPlayerButtonUp(PlayerActionButton playerActionButton)
        {
            if (playerActionButton.ActionType.IsMoveAction())
            {
                MoveTetrimino(playerActionButton.ActionType);
            }
        }

        private void OnPlayerButtonHold(PlayerActionButton playerActionButton)
        {
            if (playerActionButton.ActionType.IsMoveAction())
            {
                MoveTetrimino(playerActionButton.ActionType);
            }
        }

        private void MoveTetrimino(ActionType playerAction)
        {
            _tetriminoMover.MoveTetrimino(playerAction.TransformInputToDirection());
        }

        public void Dispose()
        {
            _playerInputController.ActionButtonUp -= OnPlayerButtonUp;
            _playerInputController.ActionButtonHold -= OnPlayerButtonHold;
        }
    }
}