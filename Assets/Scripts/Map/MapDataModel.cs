using System.Collections.Generic;
using Config;
using Data;
using Map.Cells;

namespace Map
{
    public class MapDataModel
    {
        public Dictionary<CellPosition, CellOccupancy> Grid { get; private set; } =
            new Dictionary<CellPosition, CellOccupancy>();

        public MapDataModel(MapConfig mapConfig)
        {
            Init(mapConfig.MapWidth, mapConfig.MapHeight);
        }

        private void Init(int width, int height)
        {
            Grid = new Dictionary<CellPosition, CellOccupancy>();
            for (var rowIndex = 0; rowIndex < height; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < width; columnIndex++)
                {
                    var cellPosition = new CellPosition(columnIndex, rowIndex);

                    Grid.Add(cellPosition, CellOccupancy.Empty);
                }
            }
        }
        
        public void UpdateCells(IEnumerable<CellPosition> cellPositions, CellOccupancy occupancy)
        {
            foreach (var cellPosition in cellPositions)
            {
                SetCell(cellPosition, occupancy);
            }
        }

        public CellOccupancy GetCellOccupancy(CellPosition cellPosition)
        {
            if (Grid.ContainsKey(cellPosition))
            {
                return Grid[cellPosition];
            }
            
            //throw new ArgumentOutOfRangeException($"Cannot get cell [{cellPosition.X}][{cellPosition.Y}] from grid");

            return CellOccupancy.Empty;
        }

        private void SetCell(CellPosition cellPosition, CellOccupancy cellOccupancy)
        {
            Grid[cellPosition] = cellOccupancy;
        }
    }
}