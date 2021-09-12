using Tetrimino.Data;
using TetriminoProvider.Infrastructure;

namespace TetriminoProvider.Implementation
{
	public class SpecificTetriminoProvider : ITetriminoesProvider
	{
		public TetriminoType GetPiece()
		{
			return TetriminoType.I;
		}
	}
}