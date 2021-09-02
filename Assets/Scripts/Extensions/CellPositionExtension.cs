using System;
using Config;
using Data;
using TetriminoMoving;
using TetriminoMoving.Data;
using UnityEngine;

namespace Extensions
{
    public static class CellPositionExtension
    {
        public static Vector3 GetCellWorldPosition(this CellPosition cellPosition, MapConfig mapConfig)
        {
            var x = cellPosition.X * mapConfig.CellSpacing + cellPosition.X * mapConfig.CellSize;
            var y = cellPosition.Y * mapConfig.CellSpacing + cellPosition.Y * mapConfig.CellSize;
            return new Vector3(x, y, 0);
        }

        public static CellPosition Move(this CellPosition cellPosition, MoveDirection moveDirection)
        {
            var x = 0;
            var y = 0;
            switch (moveDirection)
            {
                case MoveDirection.Down:
                    y = -1;
                    break;
                case MoveDirection.Left:
                    x = -1;
                    break;
                case MoveDirection.Right:
                    x = +1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(moveDirection), moveDirection, null);
            }

            return new CellPosition(cellPosition.X + x, cellPosition.Y + y);
        }

        public static bool IsInBounds(this CellPosition cellPosition, MapConfig mapConfig)
        {
            var isXInBounds = IsXInBounds(cellPosition, mapConfig.MapWidth);
            return isXInBounds && cellPosition.Y >= 0;
        }

        private static bool IsXInBounds(CellPosition cellPosition, int xBounds)
        {
            return cellPosition.X >= 0 && cellPosition.X < xBounds;
        }
    }
}