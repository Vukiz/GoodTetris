using System.Collections.Generic;
using System.Linq;
using Data;
using Extensions;
using Map.Cells;
using Tetrimino.Data;
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

		public IEnumerable<CellMoveData> PartsPositionsInWorldCoordsWithRotation(RotateDirection rotateDirection)
		{
			var rotatedParts = PartsHolder.PartsWorldPositions.RotateWithMatrix(rotateDirection, TetriminoPosition, RotationPoint);
			return rotatedParts;
		}
	}
}