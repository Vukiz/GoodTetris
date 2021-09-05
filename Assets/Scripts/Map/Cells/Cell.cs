using System.Diagnostics;
using Data;
using Tetrimino;
using Zenject;

namespace Map.Cells
{
	[DebuggerDisplay("IsFilled = {IsFilled}")]
	public class Cell
	{
		private readonly MapDrawer _mapDrawer;
		public CellPosition CellPosition { get; }

		public CellOccupancy CellOccupancy =>
			OccupiedTetriminoView == null ? CellOccupancy.Empty : CellOccupancy.Filled;

		private string IsFilled => CellOccupancy == CellOccupancy.Filled? "Filled":"Empty";

		public TetriminoPartView OccupiedTetriminoView { get; private set; }
		

		public Cell(CellPosition cellPosition, MapDrawer mapDrawer)
		{
			_mapDrawer = mapDrawer;
			CellPosition = cellPosition;
		}

		public void SetPartView(TetriminoPartView tetriminoPartView)
		{
			OccupiedTetriminoView = tetriminoPartView;

			var isPartNotNull = tetriminoPartView != null;
			if (isPartNotNull)
			{
				tetriminoPartView.MoveToNewPosition(CellPosition);
				_mapDrawer.GetCellView(CellPosition).SetText(CellPosition);
			}
			else
			{
				_mapDrawer.GetCellView(CellPosition).SetText();
			}

			_mapDrawer.GetCellView(CellPosition).SetPartDebugActive(isPartNotNull);
		}
	}
}