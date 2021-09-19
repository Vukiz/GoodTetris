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

	#if UNITY_EDITOR
		private readonly MapDrawer _mapDrawer;
		
		public Cell(CellPosition cellPosition, MapDrawer mapDrawer)
		{
			_mapDrawer = mapDrawer;
			CellPosition = cellPosition;
		}
	#else
		public Cell(CellPosition cellPosition)
		{
			CellPosition = cellPosition;
		}
	#endif

		public void SetPartView(TetriminoPartView tetriminoPartView)
		{
			OccupiedTetriminoView = tetriminoPartView;

			var isPartNotNull = tetriminoPartView != null;

			if (isPartNotNull)
			{
				tetriminoPartView.SetWorldPosition(CellPosition);
			}

		#if UNITY_EDITOR
			if (isPartNotNull)
			{
				_mapDrawer.GetCellView(CellPosition).SetText(CellPosition);
			}
			else
			{
				_mapDrawer.GetCellView(CellPosition).SetText();
			}

			_mapDrawer.GetCellView(CellPosition).SetPartDebugActive(isPartNotNull);
		#endif
		}
	}
}