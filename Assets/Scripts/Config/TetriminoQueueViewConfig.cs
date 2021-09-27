using System;
using Data;

namespace Config
{
	[Serializable]
	public class TetriminoQueueViewConfig
	{
		public CellPosition StartPosition;
		public CellPosition Spacing;
		public int HowManyTetriminoesToShow;
	}
}