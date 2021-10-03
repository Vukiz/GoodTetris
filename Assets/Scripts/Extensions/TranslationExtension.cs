using System.Collections.Generic;
using System.Linq;
using Data;

namespace Extensions
{
	public static class TranslationExtension
	{
		public static TetriminoCalculationPoint TranslateToPoint(this CellPosition cellPosition,
			TetriminoCalculationPoint pointToTranslateInto)
		{
			return cellPosition + pointToTranslateInto;
		}

		public static CellPosition TranslateToPoint(this TetriminoCalculationPoint cellPosition,
			TetriminoCalculationPoint pointToTranslateInto)
		{
			var calculationPoint = cellPosition + pointToTranslateInto;
			return new CellPosition((int) calculationPoint.X, (int) calculationPoint.Y);
		}


		public static IEnumerable<CellPosition> OffsetPosition(this IEnumerable<CellPosition> initialPositions,
			CellPosition offset)
		{
			return initialPositions.Select(initialPosition => initialPosition + offset);
		}
	}
}