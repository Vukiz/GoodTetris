using System.Linq;
using Data;
using Extensions;
using Tetrimino.Data;

namespace Tetrimino
{
    public class TetriminoDataModel
    {
        private TetriminoParts Parts { get; }
        public CellPosition TetriminoCenter => CellPosition.Zero;
        public TetriminoType TetriminoType { get; set; }
        public TetriminoRotation TetriminoRotation { get; set; }
        public CellPosition TetriminoPosition { get; set; }

        public TetriminoParts RotatedParts => Parts.Rotate(TetriminoRotation);

        public TetriminoDataModel(TetriminoParts tetriminoParts)
        {
            Parts = tetriminoParts;
        }

        public TetriminoParts AllPartsInWorldCoords(CellPosition relativeCellPosition)
        {
            return AllPartsInWorldCoords(relativeCellPosition, TetriminoRotation);
        }

        public TetriminoParts AllPartsInWorldCoords(TetriminoRotation newTetriminoRotation)
        {
            return AllPartsInWorldCoords(TetriminoPosition, newTetriminoRotation);
        }

        public TetriminoParts AllPartsInWorldCoords()
        {
            return AllPartsInWorldCoords(TetriminoPosition, TetriminoRotation);
        }

        private TetriminoParts AllPartsInWorldCoords(CellPosition relativeCellPosition,
            TetriminoRotation newTetriminoRotation)
        {
            var rotatedParts = Parts.Rotate(newTetriminoRotation);
            var cellPositions = rotatedParts.PartsPositions.Select(p => p + relativeCellPosition).ToList();
            cellPositions.Add(TetriminoCenter + relativeCellPosition);
            return new TetriminoParts(cellPositions);
        }
    }
}