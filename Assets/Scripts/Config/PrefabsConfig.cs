using System;
using DropPositionHighlighting;
using Map.Cells;
using Player;
using Tetrimino;

namespace Config
{
    [Serializable]
    public class PrefabsConfig
    {
        public CellView CellPrefab;
        public DropCellView DropCellPrefab;
        public TetriminoView TetriminoView;
        public TetriminoPartView TetriminoPart;
        public PlayerInputController PlayerInputController;
    }
}