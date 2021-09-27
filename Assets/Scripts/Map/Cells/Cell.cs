using System.Diagnostics;
using Data;
using Tetrimino;

namespace Map.Cells
{
	[DebuggerDisplay("IsFilled = {IsFilled}")]
	public class Cell
	{
		public CellPosition CellPosition { get; }

		public CellOccupancy CellOccupancy =>
			OccupiedTetriminoView == null ? CellOccupancy.Empty : CellOccupancy.Filled;

		private string IsFilled => CellOccupancy == CellOccupancy.Filled ? "Filled" : "Empty";

		public override string ToString()
		{
			return $"Cell: {CellPosition.X}|{CellPosition.Y} ";
		}

		public TetriminoPartView OccupiedTetriminoView { get; private set; }

		public Cell(CellPosition cellPosition)
		{
			CellPosition = cellPosition;
		}

		public void SetPartView(TetriminoPartView tetriminoPartView)
		{
			OccupiedTetriminoView = tetriminoPartView;

			var isPartNotNull = tetriminoPartView != null;

			if (isPartNotNull)
			{
				tetriminoPartView.SetWorldPosition(CellPosition);
			}
		}
	}
}