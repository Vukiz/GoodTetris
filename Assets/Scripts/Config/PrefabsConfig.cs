using System;
using Map.Cells;
using Player;
using Tetrimino;

namespace Config
{
    [Serializable]
    public class PrefabsConfig
    {
        public CellView CellPrefab;
        public TetriminoView TetriminoView;
        public TetriminoPartView TetriminoPart;
        public PlayerInputController PlayerInputController;
    }
}