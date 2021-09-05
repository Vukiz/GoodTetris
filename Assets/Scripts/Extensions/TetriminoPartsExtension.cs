using System;
using System.Collections.Generic;
using Data;
using Map.Cells;
using Tetrimino.Data;

namespace Extensions
{
	public static class TetriminoPartsExtension
	{
		public static IEnumerable<CellMoveData> RotateWithMatrix(
			this TetriminoPartsHolder nonRotatedTetriminoPartsHolder, TetriminoRotation rotation)
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

			var list = new List<CellMoveData>();
			foreach (var kvp in nonRotatedTetriminoPartsHolder.Parts)
			{
				var rotatedCellPosition = GetRotatedCellPosition(kvp.Key, sinus, cosinus);
				list.Add(new CellMoveData(kvp.Key, rotatedCellPosition));
			}

			return list;
		}

		private static CellPosition GetRotatedCellPosition(CellPosition cellPosition, int sinus, int cosinus)
		{
			var x = cellPosition.X * cosinus - cellPosition.Y * sinus;
			var y = cellPosition.X * sinus + cellPosition.Y * cosinus;
			return new CellPosition(x, y);
		}
	}
}