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
			var rotatedParts = PartsHolder.PartsPositions.RotateWithMatrix(TetriminoRotation);

			return (from rotatedPart in rotatedParts
				let oldPosition = TetriminoPosition + rotatedPart.PositionTransformation.NewPosition
				let newPosition = relativeCellPosition + rotatedPart.PositionTransformation.NewPosition
				select new CellMoveData(oldPosition, newPosition)).ToList();
		}

		public IEnumerable<CellMoveData> PartsPositionsInWorldCoordsWithRotation(RotateDirection rotateDirection)
		{
			var rotatedParts = PartsHolder.PartsPositions.RotateWithMatrix(TetriminoRotation)
				.Select(p => p.PositionTransformation.NewPosition).RotateWithMatrix(rotateDirection);

			return (from rotatedPart in rotatedParts
				let oldPosition = TetriminoPosition + rotatedPart.PositionTransformation.OldPosition
				let newPosition = TetriminoPosition + rotatedPart.PositionTransformation.NewPosition
				select new CellMoveData(oldPosition, newPosition)).ToList();
		}
	}
}