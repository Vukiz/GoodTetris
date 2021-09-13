using System;
using Extensions;
using TetriminoMoving;
using Zenject;

namespace Player
{
	public class PlayerTetriminoMover : IInitializable, IDisposable
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
			_playerInputController.ActionButtonHold += OnPlayerHold;
		}

		private void OnPlayerButtonUp(PlayerActionButton playerActionButton)
		{
			ProcessMoveAction(playerActionButton);
			if (playerActionButton.ActionType == ActionType.Drop)
			{
				DropTetrimino();
			}
		}

		private void OnPlayerHold(PlayerActionButton playerActionButton)
		{
			ProcessMoveAction(playerActionButton);
		}

		private void ProcessMoveAction(PlayerActionButton playerActionButton)
		{
			if (playerActionButton.ActionType.IsMoveAction())
			{
				MoveTetrimino(playerActionButton.ActionType);
			}
		}

		private void DropTetrimino()
		{
			_tetriminoMover.DropTetrimino();
		}

		private void MoveTetrimino(ActionType playerAction)
		{
			_tetriminoMover.MoveTetriminoInDirection(playerAction.TransformInputToDirection());
		}

		public void Dispose()
		{
			_playerInputController.ActionButtonUp -= OnPlayerButtonUp;
		}
	}
}