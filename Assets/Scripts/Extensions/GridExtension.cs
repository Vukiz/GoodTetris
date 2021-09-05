using System.Collections.Generic;
using System.Linq;
using Map.Cells;

namespace Extensions
{
	public static class GridExtension
	{
		public static bool IsRowEmpty(this IEnumerable<Cell> row)
		{
			return row.All(c => c.CellOccupancy == CellOccupancy.Empty);
		}
		
		public static bool IsRowFilled(this IEnumerable<Cell> row)
		{
			return row.All(c => c.CellOccupancy == CellOccupancy.Filled);
		}
	}
}