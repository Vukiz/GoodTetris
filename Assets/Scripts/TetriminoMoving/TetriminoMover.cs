using System;
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
using UnityEngine;

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

		public void MoveTetriminoInDirection(MoveDirection moveDirection)
		{
			var currentTetrimino = _currentTetriminoManager.CurrentTetrimino;
			if (currentTetrimino == null)
			{
				return;
			}

			MoveTetrimino(moveDirection, currentTetrimino);
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

			UpdateMapCells(dropPosition);
			tetrimino.SetNewTetriminoPosition(tetriminoPosition);
			TetriminoDown();
		}

		public void RotateTetrimino(RotateDirection rotateDirection)
		{
			var currentTetrimino = _currentTetriminoManager.CurrentTetrimino;
			if (currentTetrimino == null)
			{
				return;
			}

			var newTetriminoRotation = currentTetrimino.Model.NextRotation(rotateDirection);
			var newParts = currentTetrimino.Model.PartsPositionsInWorldCoordsWithRotation(rotateDirection)
				.ToList();

			if (!IsTetriminoRotateConfigValid(newParts))
			{
				return;
			}

			UpdateMapCells(newParts);
			currentTetrimino.SetNewTetriminoRotation(newTetriminoRotation);
		}

		private void MoveTetrimino(MoveDirection moveDirection, TetriminoHolder tetriminoHolder)
		{
			var currentTetriminoPosition = tetriminoHolder.Model.TetriminoPosition;
			var newCellPosition = currentTetriminoPosition.Move(moveDirection);

			var newPartsTransformations = tetriminoHolder.Model.PartsTransformationRelativeTo(newCellPosition).ToList();

			var isTetriminoInBounds = IsTetriminoInBounds(newPartsTransformations);
			if (!isTetriminoInBounds)
			{
				if (!IsAnyPartBelowTheFloor(newPartsTransformations))
				{
					return;
				}

				TetriminoDown();
				return;
			}

			if (!IsMapSpaceEmpty(newPartsTransformations))
			{
				TetriminoDown();
				return;
			}

			UpdateMapCells(newPartsTransformations);
			tetriminoHolder.SetNewTetriminoPosition(newCellPosition);
		}

		private IEnumerable<CellMoveData> GetDropPosition(TetriminoHolder tetriminoHolder,
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

			return tetriminoHolder.Model.PartsTransformationRelativeTo(lastValidPos).ToList();
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