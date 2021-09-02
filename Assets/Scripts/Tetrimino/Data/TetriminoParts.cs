using System.Collections.Generic;
using Data;

namespace Tetrimino.Data
{
    public class TetriminoParts
    {
        public IReadOnlyList<CellPosition> PartsPositions { get; }

        public TetriminoParts(IReadOnlyList<CellPosition> partsPositions)
        {
            PartsPositions = partsPositions;
        }
    }
}