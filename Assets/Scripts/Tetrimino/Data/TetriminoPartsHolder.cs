using System.Collections.Generic;
using System.Linq;
using Data;

namespace Tetrimino.Data
{
	public class TetriminoPartsHolder
	{
		public List<TetriminoPartView> Parts { get; } = new List<TetriminoPartView>();

		public IEnumerable<CellPosition> PartsWorldPositions => Parts.Select(p => p.WorldCellPosition);

		public void AddPart(TetriminoPartView partView)
		{
			Parts.Add(partView);
		}

		public void RemovePart(TetriminoPartView partView)
		{
			Parts.Remove(partView);
		}
	}
}