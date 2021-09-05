using System;
using System.Diagnostics;

namespace Data
{
    [Serializable]
    [DebuggerDisplay("{X}|{Y}")]
    public struct CellPosition
    {
        public int X;
        public int Y;

        public CellPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static CellPosition Zero { get; } = new CellPosition(0,0 );

        public static CellPosition operator +(CellPosition cellPosition, CellPosition cellPosition2)
        {
            return new CellPosition(cellPosition.X + cellPosition2.X, cellPosition.Y + cellPosition2.Y);
        }

        public static bool operator ==(CellPosition cellPosition, CellPosition cellPosition2)
        {
            return cellPosition.X == cellPosition2.X && cellPosition.Y == cellPosition2.Y;
        }

        public static bool operator !=(CellPosition cellPosition, CellPosition cellPosition2)
        {
            return !(cellPosition == cellPosition2);
        }
    }
}