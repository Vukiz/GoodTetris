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
	}
}