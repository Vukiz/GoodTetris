using Data;
using Tetrimino;
using Tetrimino.Data;

namespace Spawner.Infrastructure
{
    public interface ITetriminoFactory
    {
        TetriminoHolder CreateTetrimino(TetriminoType tetriminoType, CellPosition newTetriminoPosition);
    }
}