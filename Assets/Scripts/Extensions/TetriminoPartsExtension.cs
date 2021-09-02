using System;
using System.Linq;
using Data;
using Tetrimino.Data;

namespace Extensions
{
    public static class TetriminoPartsExtension
    {
        public static TetriminoParts Rotate(this TetriminoParts nonRotatedTetriminoParts, TetriminoRotation rotation)
        {
            int sinus;
            int cosinus;
            switch (rotation)
            {
                case TetriminoRotation.Up:
                    return nonRotatedTetriminoParts;
                case TetriminoRotation.Right:
                    sinus = 1;
                    cosinus = 0;
                    break;
                case TetriminoRotation.Down:
                    sinus = 0;
                    cosinus = -1;
                    break;
                case TetriminoRotation.Left:
                    sinus = -1;
                    cosinus = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rotation), rotation, null);
            }

            var partsPositions = nonRotatedTetriminoParts.PartsPositions.Select(pp =>
                GetRotatedCellPosition(pp, cosinus, sinus));
            return new TetriminoParts(partsPositions.ToList());
        }

        private static CellPosition GetRotatedCellPosition(CellPosition cellPosition, int sinus, int cosinus)
        {
            var x = cellPosition.X * cosinus - cellPosition.Y * sinus;
            var y = cellPosition.X * sinus + cellPosition.Y * cosinus;
            return new CellPosition(x, y);
        }
    }
}