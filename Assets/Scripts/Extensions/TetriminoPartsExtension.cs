using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Map.Cells;
using Tetrimino.Data;
using TetriminoMoving.Data;

namespace Extensions
{
	public static class TetriminoPartsExtension
	{
		public static IEnumerable<CellMoveData> RotateWithMatrix(this IEnumerable<CellPosition> oldCellPositions,
			RotateDirection rotateDirection)
		{
			var sinus = rotateDirection switch
			{
				RotateDirection.Clockwise => 1,
				RotateDirection.Counterclockwise => -1,
				_ => throw new ArgumentOutOfRangeException(nameof(rotateDirection), rotateDirection, null)
			};

			var list = new List<CellMoveData>();
			foreach (var kvp in oldCellPositions)
			{
				var rotatedCellPosition = GetRotatedCellPosition(kvp, sinus);
				list.Add(new CellMoveData(kvp, rotatedCellPosition));
			}

			return list;
		}

		private static CellPosition GetRotatedCellPosition(CellPosition cellPosition, int sinus)
		{
			var x = -cellPosition.Y * sinus;
			var y = cellPosition.X * sinus;
			return new CellPosition(x, y);
		}

		public static IEnumerable<CellMoveData> RotateWithMatrix(
			this IEnumerable<CellPosition> oldCellPositions, TetriminoRotation rotation)
		{
			int sinus;
			int cosinus;
			switch (rotation)
			{
				case TetriminoRotation.Up:
					sinus = 0;
					cosinus = 1;
					break;
				case TetriminoRotation.Right:
					sinus = 1;
					cosinus = 0;
					break;
				case TetriminoRotation.Down:
					sinus = 0;
					cosinus = -1;
					break;
				case TetriminoRotation.Left:
					sinus = -1;
					cosinus = 0;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(rotation), rotation, null);
			}

			return (from kvp in oldCellPositions
				let rotatedCellPosition = GetRotatedCellPosition(kvp, sinus, cosinus)
				select new CellMoveData(kvp, rotatedCellPosition)).ToList();
		}

		private static CellPosition GetRotatedCellPosition(CellPosition cellPosition, int sinus, int cosinus)
		{
			var x = cellPosition.X * cosinus - cellPosition.Y * sinus;
			var y = cellPosition.X * sinus + cellPosition.Y * cosinus;
			return new CellPosition(x, y);
		}
	}
}