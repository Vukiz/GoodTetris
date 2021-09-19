using System.Collections.Generic;
using System.Linq;
using Config;
using Data;
using Extensions;
using Map.Cells;
using Zenject;

namespace Map
{
    public class MapDrawer
    {
        private MapDataModel _mapDataModel;
        private CellView.Factory _cellViewFactory;
        private MapConfig _mapConfig;

        private readonly Dictionary<CellPosition, CellView> _drawnCells = new Dictionary<CellPosition, CellView>();

        [Inject]
        private void Construct(MapConfig mapConfig, CellView.Factory cellViewFactory, MapDataModel mapDataModel)
        {
            _mapConfig = mapConfig;
            _cellViewFactory = cellViewFactory;
            _mapDataModel = mapDataModel;
        #if UNITY_EDITOR
            _mapDataModel.SetDrawer(this);
        #endif
        }

        public void Draw()
        {
            var grid = _mapDataModel.Grid;

            foreach (var cell in grid.SelectMany(row => row))
            {
                DrawCell(cell.CellPosition);
            }
        }

        public CellView GetCellView(CellPosition cellPosition)
        {
            return _drawnCells[cellPosition];
        }

        private void DrawCell(CellPosition cellPosition)
        {
            CellView cellView;

            if (_drawnCells.ContainsKey(cellPosition))
            {
                cellView = _drawnCells[cellPosition];
            }
            else
            {
                cellView = _cellViewFactory.Create();
                cellView.transform.position = cellPosition.GetCellWorldPosition(_mapConfig);
                _drawnCells.Add(cellPosition, cellView);
            }

            cellView.Draw();
        }
    }
}