using System;
using System.Collections.Generic;
using Data;
using Tetrimino;
using Tetrimino.Data;

namespace Config
{
    [Serializable]
    public class TetriminoesConfig
    {
        public List<TetriminoConfig> Tetriminoes;
    }

    [Serializable]
    public class TetriminoConfig
    {
        public TetriminoType TetriminoType;
        public List<CellPosition> TetriminoParts;
    }
}