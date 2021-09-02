using System;
using Extensions;
using TetriminoMoving;
using Zenject;

namespace Player
{
    public class PlayerTetriminoRotator : IInitializable, IDisposable
    {
        private readonly PlayerInputController _playerInputController;
        private readonly TetriminoMover _tetriminoMover;

        public PlayerTetriminoRotator(PlayerInputController playerInputController, TetriminoMover tetriminoMover)
        {
            _playerInputController = playerInputController;
            _tetriminoMover = tetriminoMover;
        }
        
        public void Initialize()
        {
            _playerInputController.ActionButtonUp += OnPlayerButtonUp;
        }

        public void Dispose()
        {
            _playerInputController.ActionButtonUp -= OnPlayerButtonUp;
        }

        private void OnPlayerButtonUp(PlayerActionButton playerActionButton)
        {
            if (playerActionButton.ActionType.IsRotateAction())
            {
                RotatePlayerTetrimino(playerActionButton.ActionType);
            }
        }

        private void RotatePlayerTetrimino(ActionType actionType)
        {
            _tetriminoMover.RotateTetrimino(actionType.TransformInputToRotation());
        }
    }
}