using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Tetrimino.Data;

namespace Tetrimino.Helpers
{
	public static class WallKickHelper
	{
		//https://tetris.fandom.com/wiki/SRS
		private static readonly List<CellPosition> JLTSZUpRightTests =
			new List<CellPosition>
			{
				new CellPosition(-1, 0),
				new CellPosition(-1, 1),
				new CellPosition(0, -2),
				new CellPosition(-1, -2),
			};

		private static readonly List<CellPosition> JLTSZDownLeftTests =
			new List<CellPosition>
			{
				new CellPosition(1, 0),
				new CellPosition(1, 1),
				new CellPosition(0, -2),
				new CellPosition(1, -2),
			};
		
		// ReSharper disable once InconsistentNaming
		private static readonly List<CellPosition> IUpRightTests =
			new List<CellPosition>
			{
				new CellPosition(-2, 0),
				new CellPosition(1, 0),
				new CellPosition(-2, -1),
				new CellPosition(1, 2),
			};
		
		// ReSharper disable once InconsistentNaming
		private static readonly List<CellPosition> IRightDownTests =
			new List<CellPosition>
			{
				new CellPosition(-1, 0),
				new CellPosition(2, 0),
				new CellPosition(-1, 2),
				new CellPosition(2, -1),
			};

		public static IEnumerable<CellPosition> GetTestsByRotations(
			TetriminoType tetriminoType,
			TetriminoRotation tetriminoRotation,
			TetriminoRotation nextRotation)
		{
			return tetriminoType == TetriminoType.I
				? GetTestsByRotationsForI(tetriminoRotation, nextRotation)
				: GetTestsByRotationsForJLTSZ(tetriminoRotation, nextRotation);
		}

		private static List<CellPosition> NegateTests(IEnumerable<CellPosition> tests)
		{
			return tests.Select(cellPosition => -cellPosition).ToList();
		}

		private static List<CellPosition> GetTestsByRotationsForI(TetriminoRotation tetriminoRotation,
			TetriminoRotation nextRotation)
		{
			switch (tetriminoRotation, nextRotation)
			{
				case (TetriminoRotation.Up, TetriminoRotation.Right):
				case (TetriminoRotation.Left, TetriminoRotation.Down):
				{
					return IUpRightTests;
				}
				case (TetriminoRotation.Right, TetriminoRotation.Up):
				case (TetriminoRotation.Down, TetriminoRotation.Left):
				{
					return NegateTests(IUpRightTests);
				}
				case (TetriminoRotation.Up, TetriminoRotation.Left):
				case (TetriminoRotation.Right, TetriminoRotation.Down):
				{
					return IRightDownTests;
				}
				case (TetriminoRotation.Left, TetriminoRotation.Up):
				case (TetriminoRotation.Down, TetriminoRotation.Right):
				{
					return NegateTests(IRightDownTests);
				}
			}
			
			throw new ArgumentException(
				$"Impossible rotation passed. Cannot get wall-kick tests for {tetriminoRotation} into {nextRotation}");
		}

		private static List<CellPosition> GetTestsByRotationsForJLTSZ(
			TetriminoRotation tetriminoRotation,
			TetriminoRotation nextRotation)
		{
			switch (tetriminoRotation, nextRotation)
			{
				case (TetriminoRotation.Up, TetriminoRotation.Right):
				case (TetriminoRotation.Down, TetriminoRotation.Right):
				{
					return JLTSZUpRightTests;
				}
				case (TetriminoRotation.Right, TetriminoRotation.Up):
				case (TetriminoRotation.Right, TetriminoRotation.Down):
				{
					return NegateTests(JLTSZUpRightTests);
				}
				case (TetriminoRotation.Down, TetriminoRotation.Left):
				case (TetriminoRotation.Up, TetriminoRotation.Left):
				{
					return JLTSZDownLeftTests;
				}
				case (TetriminoRotation.Left, TetriminoRotation.Down):
				case (TetriminoRotation.Left, TetriminoRotation.Up):
				{
					return NegateTests(JLTSZDownLeftTests);
				}
			}

			throw new ArgumentException(
				$"Impossible rotation passed. Cannot get wall-kick tests for {tetriminoRotation} into {nextRotation}");
		}
	}
}