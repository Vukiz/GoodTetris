using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Map.Cells;
using TetriminoMoving.Data;

namespace Extensions
{
	public static class TetriminoPartsExtension
	{
		public static IEnumerable<CellMoveData> RotateWithMatrix(this IEnumerable<CellPosition> oldCellPositions,
			RotateDirection rotateDirection, CellPosition tetriminoPosition, TetriminoCalculationPoint originPoint)
		{
			var sinus = rotateDirection switch
			{
				RotateDirection.Clockwise => -1,
				RotateDirection.Counterclockwise => 1,
				_ => throw new ArgumentOutOfRangeException(nameof(rotateDirection), rotateDirection, null)
			};

			var list = new List<CellMoveData>();
			var translationPoint = tetriminoPosition + originPoint;
			foreach (var cellPosition in oldCellPositions)
			{
				var cellPosTranslatedToTheOrigin = cellPosition.TranslateToPoint(-translationPoint);
				var rotatedCellPosition = GetRotatedCellPosition(cellPosTranslatedToTheOrigin, sinus);
				var cellPosTranslatedBack = rotatedCellPosition.TranslateToPoint(translationPoint);
				list.Add(new CellMoveData(cellPosition, cellPosTranslatedBack));
			}

			return list;
		}

		private static TetriminoCalculationPoint GetRotatedCellPosition(TetriminoCalculationPoint cellPosition, int sinus)
		{
			var x = -cellPosition.Y * sinus;
			var y = cellPosition.X * sinus;
			return new TetriminoCalculationPoint(x, y);
		}
	}
}