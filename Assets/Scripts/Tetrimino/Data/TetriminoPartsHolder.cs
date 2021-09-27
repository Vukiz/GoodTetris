using System.Collections.Generic;
using System.Linq;
using Data;

namespace Tetrimino.Data
{
	public class TetriminoPartsHolder
	{
		public List<TetriminoPartView> Parts { get; } = new List<TetriminoPartView>();

		public IEnumerable<CellPosition> PartsWorldPositions => Parts.Select(p => p.WorldCellPosition);

		public void AddParts(IEnumerable<TetriminoPartView> partViews)
		{
			Parts.AddRange(partViews);
		}

		public void RemovePart(TetriminoPartView partView)
		{
			Parts.Remove(partView);
		}
	}
}