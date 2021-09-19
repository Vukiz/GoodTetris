using System;
using DropPositionHighlighting;
using Map.Cells;
using Player;
using Tetrimino;
using TetriminoQueue;

namespace Config
{
    [Serializable]
    public class PrefabsConfig
    {
        public TetriminoQueueView QueueViewPrefab;
        public CellView CellPrefab;
        public DropCellView DropCellPrefab;
        public TetriminoView TetriminoView;
        public TetriminoPartView TetriminoPart;
        public PlayerInputController PlayerInputController;
    }
}