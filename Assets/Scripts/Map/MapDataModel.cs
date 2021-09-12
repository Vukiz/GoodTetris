using System;
using System.Collections.Generic;
using System.Linq;
using Config;
using Data;
using Extensions;
using Map.Cells;
using Tetrimino;
using UnityEngine;
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
			return cell == null || cell.CellOccupancy == CellOccupancy.Filled;
		}

		public void CollapseEmptyLines()
		{
			var emptyRowIndex = -1;
			for (var rowIndex = 0; rowIndex < Grid.Count; rowIndex++)
			{
				var row = Grid[rowIndex];
				if (row.IsRowEmpty())
				{
					if (emptyRowIndex != -1)
					{
						continue;
					}

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
					emptyRowIndex = -1;
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

			//var clearedCells = cellsToClear.Aggregate("", (s, cell) => cell + s + " ");
			//Debug.Log($"Cleared : {clearedCells}");

			foreach (var (tetriminoPartView, cellToMoveInto) in cellsToMove)
			{
				cellToMoveInto.SetPartView(tetriminoPartView);
			}

			//var setCells = cellsToMove.Aggregate("", (s, cell) => cell.Item2 + s + " ");
			//Debug.Log($"Set : {setCells}");
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
				var upperRow = Grid[rowIndex];
				var lowerRow = Grid[rowIndex - emptyRowsCount];

				if (upperRow.IsRowEmpty())
				{
					break;
				}

				for (var cellIndex = 0; cellIndex < upperRow.Count; cellIndex++)
				{
					SwapCells(lowerRow[cellIndex], upperRow[cellIndex]);
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