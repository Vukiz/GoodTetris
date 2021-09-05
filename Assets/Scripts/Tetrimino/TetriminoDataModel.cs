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
		public TetriminoPartsHolder PartsHolder { get; }
		public CellPosition TetriminoCenter => CellPosition.Zero;
		public TetriminoType TetriminoType { get; set; }
		public TetriminoRotation TetriminoRotation { get; set; }
		public CellPosition TetriminoPosition { get; set; }

		public TetriminoRotation NextRotation(RotateDirection rotateDirection) =>
			TetriminoRotation.Rotate(rotateDirection);

		public TetriminoDataModel(TetriminoPartsHolder tetriminoPartsHolder)
		{
			PartsHolder = tetriminoPartsHolder;
		}

		public IEnumerable<CellMoveData> PartsTransformationRelativeTo(CellPosition relativeCellPosition)
		{
			return AllPartsInWorldCoords(relativeCellPosition, TetriminoRotation);
		}

		public IEnumerable<CellMoveData> PartsPositionsInWorldCoordsWithRotation(TetriminoRotation newTetriminoRotation)
		{
			return AllPartsInWorldCoords(TetriminoPosition, newTetriminoRotation);
		}

		private IEnumerable<CellMoveData> AllPartsInWorldCoords(CellPosition relativeCellPosition,
			TetriminoRotation newTetriminoRotation)
		{
			var rotatedParts = PartsHolder.RotateWithMatrix(newTetriminoRotation);
			var cellPositions = new List<CellMoveData>();
			foreach (var rotatedPart in rotatedParts)
			{
				var oldPosition = TetriminoPosition + rotatedPart.PositionTransformation.OldPosition;
				var newPosition = relativeCellPosition + rotatedPart.PositionTransformation.NewPosition;
				cellPositions.Add(new CellMoveData(oldPosition, newPosition));
			}

			return cellPositions;
		}
	}
}