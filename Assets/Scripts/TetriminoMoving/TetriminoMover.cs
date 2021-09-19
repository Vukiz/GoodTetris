using System;
using System.Collections.Generic;
using System.Linq;
using Config;
using CurrentTetriminoManager;
using Data;
using Extensions;
using Game;
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
		private readonly GameTicker _gameTicker;

		public event Action TetriminoMoved;

		public TetriminoMover(MapDataModel mapDataModel,
			TetriminoManager currentTetriminoManager,
			MapConfig mapConfig,
			GameTicker gameTicker)
		{
			_mapDataModel = mapDataModel;
			_currentTetriminoManager = currentTetriminoManager;
			_mapConfig = mapConfig;
			_gameTicker = gameTicker;
		}

		public void MoveTetriminoInDirection(MoveDirection moveDirection, bool isAutoMove)
		{
			MoveTetrimino(moveDirection, isAutoMove);
		}

		public void DropTetrimino()
		{
			var currentTetrimino = _currentTetriminoManager.CurrentTetrimino;
			if (currentTetrimino == null)
			{
				return;
			}

			var tetrimino = _currentTetriminoManager.CurrentTetrimino;
			var tetriminoPosition = tetrimino.Model.TetriminoPosition;
			var dropPosition = GetDropPosition(tetrimino, ref tetriminoPosition);
			tetrimino.SetNewTetriminoPosition(tetriminoPosition);

			UpdateMapCells(dropPosition);
			_gameTicker.ResetTickTime();
		}

		public void RotateTetrimino(RotateDirection rotateDirection)
		{
			var currentTetrimino = _currentTetriminoManager.CurrentTetrimino;
			if (currentTetrimino == null)
			{
				return;
			}

			var newParts = currentTetrimino.Model.PartsPositionsInWorldCoordsWithRotation(rotateDirection)
				.ToList();

			if (!IsTetriminoRotateConfigValid(newParts))
			{
				return;
			}
			
			var newTetriminoRotation = currentTetrimino.Model.NextRotation(rotateDirection);

			currentTetrimino.SetNewTetriminoRotation(newTetriminoRotation);

			UpdateMapCells(newParts);
		}

		public IEnumerable<CellMoveData> GetDropPosition(TetriminoHolder tetriminoHolder,
			ref CellPosition relativePoint)
		{
			var newCellPosition = relativePoint;
			List<CellMoveData> newPartsTransformations;
			CellPosition lastValidPos;
			do
			{
				lastValidPos = newCellPosition;
				newCellPosition = newCellPosition.Move(MoveDirection.Down);
				newPartsTransformations = tetriminoHolder.Model.PartsTransformationRelativeTo(newCellPosition).ToList();
			} while (IsTetriminoInBounds(newPartsTransformations) && IsMapSpaceEmpty(newPartsTransformations));

			relativePoint = lastValidPos;
			return tetriminoHolder.Model.PartsTransformationRelativeTo(lastValidPos).ToList();
		}

		private void MoveTetrimino(MoveDirection moveDirection, bool isAutoMove)
		{
			var tetriminoHolder = _currentTetriminoManager.CurrentTetrimino;
			if (tetriminoHolder == null)
			{
				return;
			}

			var currentTetriminoPosition = tetriminoHolder.Model.TetriminoPosition;
			var newCellPosition = currentTetriminoPosition.Move(moveDirection);

			var newPartsTransformations = tetriminoHolder.Model.PartsTransformationRelativeTo(newCellPosition).ToList();

			var isTetriminoInBounds = IsTetriminoInBounds(newPartsTransformations);
			if (isTetriminoInBounds)
			{
				if (IsMapSpaceEmpty(newPartsTransformations))
				{
					tetriminoHolder.SetNewTetriminoPosition(newCellPosition);

					UpdateMapCells(newPartsTransformations);
				}
				else
				{
					TetriminoDown(isAutoMove);
				}
			}
			else
			{
				if (IsAnyPartBelowTheFloor(newPartsTransformations))
				{
					TetriminoDown(isAutoMove);
				}
			}
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
			TetriminoMoved?.Invoke();
		}

		private bool IsTetriminoRotateConfigValid(IReadOnlyCollection<CellMoveData> newParts)
		{
			var isTetriminoInBounds = newParts.All(p => p.PositionTransformation.NewPosition.IsInBounds(_mapConfig));
			var isMapSpaceEmpty = IsMapSpaceEmpty(newParts);

			return isTetriminoInBounds && isMapSpaceEmpty;
		}

		private void TetriminoDown(bool isAutoMove)
		{
			if (!isAutoMove)
			{
				return;
			}

			_currentTetriminoManager.TetriminoDownInvoke();
		}

		private bool IsMapSpaceEmpty(IReadOnlyCollection<CellMoveData> partsTransformation)
		{
			var allOldCellPositions = partsTransformation.Select(p => p.PositionTransformation.OldPosition);
			var allNewCellPositions = partsTransformation.Select(p => p.PositionTransformation.NewPosition);
			var onlyNewPositions = allNewCellPositions.Except(allOldCellPositions);

			return onlyNewPositions.All(p => !IsCellFilled(p));
		}

		private bool IsCellFilled(CellPosition cellPosition)
		{
			var isCellFilled = _mapDataModel.IsCellFilled(cellPosition);
			//if (isCellFilled)
			//{
			//	Debug.Log($"{cellPosition.X}|{cellPosition.Y} is filled cannot move part here");
			//}

			return isCellFilled;
		}
	}
}