using System.Collections.Generic;
using System.Linq;
using Config;
using Data;
using Extensions;
using Map.Cells;
using Tetrimino;
using Zenject;

namespace Map
{
	public class MapDataModel : IInitializable
	{
		private readonly MapConfig _mapConfig;
		public List<List<Cell>> Grid { get; private set; }
		
		private MapDrawer _mapDrawer;

		public MapDataModel(MapConfig mapConfig)
		{
			_mapConfig = mapConfig;
		}

		public void Initialize()
		{
			InitGrid();
		}

		public bool IsCellFilled(CellPosition cellPosition)
		{
			var cell = FindCellByPosition(cellPosition);
			return cell.CellOccupancy == CellOccupancy.Filled;
		}

		public void CollapseEmptyLines()
		{
			var emptyRowIndex = -1;
			for (var rowIndex = 0; rowIndex < Grid.Count; rowIndex++)
			{
				var row = Grid[rowIndex];
				if (row.IsRowEmpty())
				{
					emptyRowIndex = rowIndex;
				}
				else
				{
					if (emptyRowIndex == -1)
					{
						continue;
					}

					MoveAllUpperRowsDown(emptyRowIndex, rowIndex);
					rowIndex = emptyRowIndex;
				}
			}
		}

		public void ClearFilledLines(IEnumerable<int> filledRows)
		{
			foreach (var rowIndex in filledRows)
			{
				var row = Grid[rowIndex];
				foreach (var cell in row)
				{
					cell.OccupiedTetriminoView.Clear();
					cell.SetPartView(null);
				}
			}
		}

		public void MovePartsToNewPositions(IEnumerable<CellMoveData> moveTransformation)
		{
			var grid = Grid;
			var cellsToMove = new List<(TetriminoPartView, Cell)>();
			var cellsToClear = new List<Cell>();
			foreach (var (oldPosition, newPosition) in moveTransformation.Select(c => c.PositionTransformation))
			{
				var cellToMove = FindCellByPosition(oldPosition);
				cellsToMove.Add((cellToMove.OccupiedTetriminoView, FindCellByPosition(newPosition)));
				cellsToClear.Add(cellToMove);
			}

			foreach (var cell in cellsToClear)
			{
				cell.SetPartView(null);
			}
			
			foreach (var (tetriminoPartView, cellToMoveInto) in cellsToMove)
			{
				cellToMoveInto.SetPartView(tetriminoPartView);
			}
		}

		public void SetTetriminoPartViewToCell(CellPosition cellPosition, TetriminoPartView tetriminoPartView)
		{
			FindCellByPosition(cellPosition).SetPartView(tetriminoPartView);
		}

		private Cell FindCellByPosition(CellPosition cellPosition)
		{
			return Grid.SelectMany(g => g).FirstOrDefault(c => c.CellPosition == cellPosition);
		}

		private void InitGrid()
		{
			var width = _mapConfig.MapWidth;
			var height = _mapConfig.MapHeight;
			Grid = new List<List<Cell>>(height);
			for (var rowIndex = 0; rowIndex < height; rowIndex++)
			{
				var row = new List<Cell>(width);
				for (var columnIndex = 0; columnIndex < width; columnIndex++)
				{
					var cellPosition = new CellPosition(columnIndex, rowIndex);

					row.Add(new Cell(cellPosition, _mapDrawer));
				}

				Grid.Add(row);
			}
		}

		private void MoveAllUpperRowsDown(int firstEmptyRowIndex, int filledRowIndex)
		{
			var emptyRowsCount = filledRowIndex - firstEmptyRowIndex;
			for (var rowIndex = filledRowIndex; rowIndex < Grid.Count - 1; rowIndex++)
			{
				var filledRow = Grid[filledRowIndex];
				var lowerRow = Grid[rowIndex - emptyRowsCount];

				for (var cellIndex = 0; cellIndex < filledRow.Count; cellIndex++)
				{
					SwapCells(lowerRow[cellIndex], filledRow[cellIndex]);
				}
			}
		}

		private static void SwapCells(Cell cell1, Cell cell2)
		{
			var firstCellOccupiedTetriminoView = cell1.OccupiedTetriminoView;
			var secondCellOccupiedTetriminoView = cell2.OccupiedTetriminoView;

			cell1.SetPartView(secondCellOccupiedTetriminoView);
			cell2.SetPartView(firstCellOccupiedTetriminoView);
		}

		public void SetDrawer(MapDrawer mapDrawer)
		{
			_mapDrawer = mapDrawer;
		}
	}
}