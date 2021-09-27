using System.Collections.Generic;
using System.Linq;
using Data;
using Tetrimino.Data;

namespace Tetrimino
{
	public class TetriminoHolder
	{
		public TetriminoView View { get; set; }
		public TetriminoDataModel Model { get; set; }

		public IEnumerable<(CellPosition, TetriminoPartView)> TetriminoPartsCreationData =>
			Model.PartsHolder.Parts.Select(tetriminoPartView =>
				(tetriminoPartView.LocalCellPosition + Model.TetriminoPosition, tetriminoPartView));

		public void SetNewTetriminoPosition(CellPosition newCellPosition)
		{
			Model.TetriminoPosition = newCellPosition;
		}

		public void SetNewTetriminoRotation(TetriminoRotation newTetriminoRotation)
		{
			Model.TetriminoRotation = newTetriminoRotation;
		}
	}
}