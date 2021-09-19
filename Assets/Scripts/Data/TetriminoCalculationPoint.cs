using System;

namespace Data
{
	[Serializable]
	public struct TetriminoCalculationPoint
	{
		public float X;
		public float Y;

		public TetriminoCalculationPoint(float x, float y)
		{
			X = x;
			Y = y;
		}

		public static TetriminoCalculationPoint operator +(
			CellPosition cellPosition,
			TetriminoCalculationPoint point2)
		{
			return new TetriminoCalculationPoint(
				point2.X + cellPosition.X,
				point2.Y + cellPosition.Y
			);
		}
		
		public static TetriminoCalculationPoint operator +(
			TetriminoCalculationPoint point1,
			TetriminoCalculationPoint point2)
		{
			return new TetriminoCalculationPoint(
				point2.X + point1.X,
				point2.Y + point1.Y
			);
		}

		public static TetriminoCalculationPoint operator -(
			TetriminoCalculationPoint calculationPoint)
		{
			return new TetriminoCalculationPoint(
				-calculationPoint.X,
				-calculationPoint.Y
			);
		}
	}
}