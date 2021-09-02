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

            MoveTetrimino(moveDirection, currentTetrimino);
        }

        public void RotateTetrimino(RotateDirection rotateDirection)
        {
            var currentTetrimino = _currentTetriminoManager.CurrentTetrimino;
            if (currentTetrimino == null)
            {
                return;
            }

            var newTetriminoRotation = currentTetrimino.Model.TetriminoRotation.Rotate(rotateDirection);
            var newParts = currentTetrimino.Model.AllPartsInWorldCoords(newTetriminoRotation).PartsPositions;
            var oldParts = currentTetrimino.Model.AllPartsInWorldCoords().PartsPositions;

            if (!IsTetriminoRotateConfigValid(newParts, oldParts))
            {
                return;
            }

            currentTetrimino.Model.TetriminoRotation = newTetriminoRotation;
            currentTetrimino.View.UpdateRotatedParts();
            _mapDataModel.UpdateCells(oldParts, CellOccupancy.Empty);
            _mapDataModel.UpdateCells(newParts, CellOccupancy.Filled);
        }

        private void MoveTetrimino(MoveDirection moveDirection, TetriminoHolder tetriminoHolder)
        {
            var currentTetriminoPosition = tetriminoHolder.Model.TetriminoPosition;
            var newCellPosition = currentTetriminoPosition.Move(moveDirection);

            var newParts = tetriminoHolder.Model.AllPartsInWorldCoords(newCellPosition).PartsPositions;   
            var oldParts = tetriminoHolder.Model.AllPartsInWorldCoords().PartsPositions;

            if (!IsTetriminoConfigValid(newParts, oldParts))
            {
                return;
            }

            tetriminoHolder.View.transform.position = newCellPosition.GetCellWorldPosition(_mapConfig);
            tetriminoHolder.Model.TetriminoPosition = newCellPosition;
            _mapDataModel.UpdateCells(oldParts, CellOccupancy.Empty);
            _mapDataModel.UpdateCells(newParts, CellOccupancy.Filled);
        }

        private bool IsTetriminoRotateConfigValid(IReadOnlyCollection<CellPosition> newParts,
            IEnumerable<CellPosition> oldParts)
        {
            var isTetriminoInBounds = newParts.All(p => p.IsInBounds(_mapConfig));
            var isMapSpaceEmpty = IsMapSpaceEmpty(newParts, oldParts);

            return isTetriminoInBounds && isMapSpaceEmpty;
        }

        private bool IsTetriminoConfigValid(IReadOnlyCollection<CellPosition> newParts,
            IEnumerable<CellPosition> oldParts)
        {
            var isTetriminoInBounds = newParts.All(p => p.IsInBounds(_mapConfig));

            if (!isTetriminoInBounds)
            {
                if (newParts.Any(p => p.Y <= 0))
                {
                    _currentTetriminoManager.TetriminoDownInvoke();
                }

                return false;
            }

            var isMapSpaceEmpty = IsMapSpaceEmpty(newParts, oldParts);
            if (!isMapSpaceEmpty)
            {
                _currentTetriminoManager.TetriminoDownInvoke();
            }

            return isMapSpaceEmpty;
        }

        private bool IsMapSpaceEmpty(IEnumerable<CellPosition> newParts,
            IEnumerable<CellPosition> oldParts)
        {
            return newParts.Where(IsCellFilled).All(oldParts.Contains);
        }

        private bool IsCellFilled(CellPosition cellPosition)
        {
            return _mapDataModel.GetCellOccupancy(cellPosition) == CellOccupancy.Filled;
        }
    }
}