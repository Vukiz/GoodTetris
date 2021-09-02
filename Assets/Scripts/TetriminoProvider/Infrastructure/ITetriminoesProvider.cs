using Tetrimino;
using Tetrimino.Data;

namespace TetriminoProvider.Infrastructure
{
    public interface ITetriminoesProvider
    {
        TetriminoType GetPiece();
    }
}