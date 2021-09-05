using Config;
using Data;
using Tetrimino.Data;

namespace Tetrimino
{
	public class TetriminoHolder
	{
		public TetriminoView View { get; set; }
		public TetriminoDataModel Model { get; set; }

		public void SetNewTetriminoPosition(CellPosition newCellPosition, MapConfig mapConfig)
		{
			//View.transform.position = newCellPosition.GetCellWorldPosition(mapConfig);
			Model.TetriminoPosition = newCellPosition;
		}

		public void SetNewTetriminoRotation(TetriminoRotation newTetriminoRotation)
		{
			Model.TetriminoRotation = newTetriminoRotation;
		}
	}
}