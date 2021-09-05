using System.Collections.Generic;
using System.Linq;
using Data;

namespace Tetrimino.Data
{
	public class TetriminoPartsHolder
	{
		public Dictionary<CellPosition, TetriminoPartView> Parts { get; } =
			new Dictionary<CellPosition, TetriminoPartView>();

		public IEnumerable<CellPosition> PartsPositions => Parts.Keys;
		public IEnumerable<TetriminoPartView> PartsViews => Parts.Values;

		public TetriminoPartsHolder(IEnumerable<CellPosition> partsPositions)
		{
			foreach (var partPosition in partsPositions)
			{
				Parts.Add(partPosition, null);
			}
		}

		public void SetPart(CellPosition position, TetriminoPartView partView)
		{
			Parts[position] = partView;
		}

		public void RemovePart(TetriminoPartView partView)
		{
			var neededParts = Parts.Where(kvp => kvp.Value == partView);
			foreach (var kvp in neededParts)
			{
				Parts.Remove(kvp.Key);
			}
		}
	}
}