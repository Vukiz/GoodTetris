using System.Diagnostics;
using Data;

namespace Map.Cells
{
	[DebuggerDisplay("{PositionTransformation.OldPosition} -> {PositionTransformation.NewPosition}")]
	public struct CellMoveData
	{
		public (CellPosition OldPosition, CellPosition NewPosition) PositionTransformation;

		public CellMoveData(CellPosition oldPos, CellPosition newPosition)
		{
			PositionTransformation = (oldPos, newPosition);
		}
	}
}