using Config;
using Data;
using Spawner.Infrastructure;
using Tetrimino;
using Tetrimino.Data;

namespace Spawner.Implementation
{
    public class TetriminoFactory : ITetriminoFactory
    {
        private const TetriminoRotation DefaultTetriminoRotation = TetriminoRotation.Up;
        private readonly TetriminoView.Factory _tetriminoFactory;
        private readonly TetriminoesConfig _tetriminoesConfig;

        public TetriminoFactory(TetriminoView.Factory tetriminoFactory, TetriminoesConfig tetriminoesConfig)
        {
            _tetriminoFactory = tetriminoFactory;
            _tetriminoesConfig = tetriminoesConfig;
        }
        public TetriminoHolder CreateTetrimino(TetriminoType tetriminoType, CellPosition newTetriminoPosition)
        {
            var view = _tetriminoFactory.Create();
            var tetriminoDataModel = new TetriminoDataModel
            {
                TetriminoType = tetriminoType,
                TetriminoRotation = DefaultTetriminoRotation,
                TetriminoPosition = newTetriminoPosition,
                RotationPoint = _tetriminoesConfig.GetRotationPoint(tetriminoType)
            };

            view.Init(tetriminoDataModel, _tetriminoesConfig.GetPartsPositions(tetriminoType));
            var result = new TetriminoHolder
            {
                View = view,
                Model = tetriminoDataModel
            };
            
            return result;
        }
    }
}