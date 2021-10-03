using System.Collections.Generic;
using System.Linq;
using Data;
using Extensions;
using Map.Cells;
using Tetrimino.Data;
using Tetrimino.Helpers;
using TetriminoMoving.Data;

namespace Tetrimino
{
	public class TetriminoDataModel
	{
		private const TetriminoRotation DefaultTetriminoRotation = TetriminoRotation.Up;

		public static CellPosition TetriminoCenter => CellPosition.Zero;

		public TetriminoPartsHolder PartsHolder { get; } = new TetriminoPartsHolder();
		public TetriminoType TetriminoType { get; set; }
		public TetriminoRotation TetriminoRotation { get; set; } = DefaultTetriminoRotation;
		public CellPosition TetriminoPosition { get; set; }
		public TetriminoCalculationPoint RotationPoint { get; set; }

		public TetriminoRotation NextRotation(RotateDirection rotateDirection) =>
			TetriminoRotation.Rotate(rotateDirection);

		public IEnumerable<CellMoveData> PartsTransformationRelativeTo(CellPosition relativeCellPosition)
		{
			var partsWorldPositions = PartsHolder.PartsWorldPositions;

			return (from partPosition in partsWorldPositions
				let oldPosition = partPosition
				let newPosition = relativeCellPosition - TetriminoPosition + partPosition
				select new CellMoveData(oldPosition, newPosition)).ToList();
		}

		public List<(IEnumerable<CellMoveData> transformation, CellPosition tetriminoPosition)> PartsPositionsInWorldCoordsWithRotation(
			RotateDirection rotateDirection)
		{
			var partsWorldPositions = PartsHolder.PartsWorldPositions.ToList();
			var nextRotation = TetriminoRotation.Rotate(rotateDirection);
			var wallKickOffsets = WallKickHelper.GetTestsByRotations(TetriminoType, TetriminoRotation, nextRotation);
			var tests = new List<CellPosition> {CellPosition.Zero}; //to keep the order of tests
			tests.AddRange(wallKickOffsets);
			var result = new List<(IEnumerable<CellMoveData>, CellPosition)>();
			foreach (var test in tests)
			{
				var offsetedTetriminoPosition = TetriminoPosition + test;
				var rotatedPartsFromOffsetPoint = partsWorldPositions.OffsetPosition(test)
					.RotateWithMatrix(rotateDirection, offsetedTetriminoPosition, RotationPoint).ToList();
				for (var partIndex = 0; partIndex < rotatedPartsFromOffsetPoint.Count; partIndex++)
				{
					var positionTransformation = rotatedPartsFromOffsetPoint[partIndex];
					rotatedPartsFromOffsetPoint[partIndex] =
						new CellMoveData(partsWorldPositions[partIndex],
							positionTransformation.PositionTransformation.NewPosition);
				}

				result.Add((rotatedPartsFromOffsetPoint, offsetedTetriminoPosition));
			}

			return result;
		}
	}
}