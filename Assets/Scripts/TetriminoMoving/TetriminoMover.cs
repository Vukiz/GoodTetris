using System.Collections.Generic;
using System.Linq;
using Config;
using CurrentTetriminoManager;
using Data;
using Extensions;
using Map;
using Map.Cells;
using Tetrimino;
using TetriminoMoving.Data;

namespace TetriminoMoving
{
	public class TetriminoMover
	{
		private readonly MapDataModel _mapDataModel;
		private readonly TetriminoManager _currentTetriminoManager;
		private readonly MapConfig _mapConfig;

		public TetriminoMover(MapDataModel mapDataModel,
			TetriminoManager currentTetriminoManager,
			MapConfig mapConfig)
		{
			_mapDataModel = mapDataModel;
			_currentTetriminoManager = currentTetriminoManager;
			_mapConfig = mapConfig;
		}

		public void MoveTetrimino(MoveDirection moveDirection)
		{
			var currentTetrimino = _currentTetriminoManager.CurrentTetrimino;
			if (currentTetrimino == null)
			{
				return;
			}

			TryMoveTetrimino(moveDirection, currentTetrimino, out _);
		}

		public void DropTetrimino()
		{
			var currentTetrimino = _currentTetriminoManager.CurrentTetrimino;
			if (currentTetrimino == null)
			{
				return;
			}

			TryMoveTetrimino(MoveDirection.Down, currentTetrimino, out var tetriminoDown);
			while (!tetriminoDown)
			{
				TryMoveTetrimino(MoveDirection.Down, currentTetrimino, out tetriminoDown);
			}
		}

		public void RotateTetrimino(RotateDirection rotateDirection)
		{
			var currentTetrimino = _currentTetriminoManager.CurrentTetrimino;
			if (currentTetrimino == null)
			{
				return;
			}

			var newTetriminoRotation = currentTetrimino.Model.NextRotation(rotateDirection);
			var newParts = currentTetrimino.Model.PartsPositionsInWorldCoordsWithRotation(newTetriminoRotation)
				.ToList();

			if (!IsTetriminoRotateConfigValid(newParts))
			{
				return;
			}

			currentTetrimino.SetNewTetriminoRotation(newTetriminoRotation);

			UpdateMapCells(newParts);
		}

		private void TryMoveTetrimino(MoveDirection moveDirection, TetriminoHolder tetriminoHolder,
			out bool tetriminoDown)
		{
			tetriminoDown = false;
			var currentTetriminoPosition = tetriminoHolder.Model.TetriminoPosition;
			var newCellPosition = currentTetriminoPosition.Move(moveDirection);

			var newPartsTransformations = tetriminoHolder.Model.PartsTransformationRelativeTo(newCellPosition).ToList();

			var isTetriminoInBounds = IsTetriminoInBounds(newPartsTransformations);
			if (!isTetriminoInBounds)
			{
				if (IsAnyPartBelowTheFloor(newPartsTransformations))
				{
					tetriminoDown = true;
					TetriminoDown();
				}

				return;
			}

			var isMapSpaceEmpty = IsMapSpaceEmpty(newPartsTransformations);
			if (!isMapSpaceEmpty)
			{
				tetriminoDown = true;
				TetriminoDown();
				return;
			}

			UpdateMapCells(newPartsTransformations);
			tetriminoHolder.SetNewTetriminoPosition(newCellPosition, _mapConfig);
		}

		private bool IsTetriminoInBounds(IEnumerable<CellMoveData> newParts)
		{
			return newParts.All(p => p.PositionTransformation.NewPosition.IsInBounds(_mapConfig));
		}

		private static bool IsAnyPartBelowTheFloor(IEnumerable<CellMoveData> newParts)
		{
			return newParts.Any(p => p.PositionTransformation.NewPosition.Y <= 0);
		}

		private void UpdateMapCells(IEnumerable<CellMoveData> newPartsPositions)
		{
			_mapDataModel.MovePartsToNewPositions(newPartsPositions);
		}

		private bool IsTetriminoRotateConfigValid(IReadOnlyCollection<CellMoveData> newParts)
		{
			var isTetriminoInBounds = newParts.All(p => p.PositionTransformation.NewPosition.IsInBounds(_mapConfig));
			var isMapSpaceEmpty = IsMapSpaceEmpty(newParts);

			return isTetriminoInBounds && isMapSpaceEmpty;
		}

		private void TetriminoDown()
		{
			_currentTetriminoManager.TetriminoDownInvoke();
		}

		private bool IsMapSpaceEmpty(IReadOnlyCollection<CellMoveData> partsTransformation)
		{
			var allOldCellPositions = partsTransformation.Select(p => p.PositionTransformation.OldPosition);
			var allNewCellPositions = partsTransformation.Select(p => p.PositionTransformation.NewPosition);
			var isMapSpaceEmpty = allNewCellPositions.Except(allOldCellPositions).All(p => !IsCellFilled(p));

			return isMapSpaceEmpty;
		}

		private bool IsCellFilled(CellPosition cellPosition)
		{
			return _mapDataModel.IsCellFilled(cellPosition);
		}
	}
}