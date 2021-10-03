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

			var translationPoint = tetriminoPosition + originPoint;

			return (from cellPosition in oldCellPositions
				let cellPosTranslatedToTheOrigin = cellPosition.TranslateToPoint(-translationPoint)
				let rotatedCellPosition = GetRotatedCellPosition(cellPosTranslatedToTheOrigin, sinus)
				let cellPosTranslatedBack = rotatedCellPosition.TranslateToPoint(translationPoint)
				select new CellMoveData(cellPosition, cellPosTranslatedBack)).ToList();
		}

		private static TetriminoCalculationPoint GetRotatedCellPosition(TetriminoCalculationPoint cellPosition,
			int sinus)
		{
			var x = -cellPosition.Y * sinus;
			var y = cellPosition.X * sinus;
			return new TetriminoCalculationPoint(x, y);
		}
	}
}