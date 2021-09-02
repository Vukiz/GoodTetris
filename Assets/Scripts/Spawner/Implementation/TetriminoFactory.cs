using System;
using System.Collections.Generic;
using System.Linq;
using Config;
using Data;
using Spawner.Infrastructure;
using Tetrimino;
using Tetrimino.Data;

namespace Spawner.Implementation
{
    public class TetriminoFactory : ITetriminoFactory
    {
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
            var tetriminoDataModel = new TetriminoDataModel(CreatePartsForType(tetriminoType))
            {
                TetriminoType = tetriminoType,
                TetriminoRotation = TetriminoRotation.Up,
                TetriminoPosition = newTetriminoPosition
            };
            
            view.Init(tetriminoDataModel);
            var result = new TetriminoHolder
            {
                View = view,
                Model = tetriminoDataModel
            };
            
            return result;
        }

        private TetriminoParts CreatePartsForType(TetriminoType tetriminoType)
        {
            var tetriminoConfig = _tetriminoesConfig.Tetriminoes.FirstOrDefault(t => t.TetriminoType == tetriminoType);
            if (tetriminoConfig != null)
            {
                return new TetriminoParts(tetriminoConfig.TetriminoParts);
            }

            throw new KeyNotFoundException(
                $"Couldn't find parts for {Enum.GetName(typeof(TetriminoType), tetriminoType)}");
        }
    }
}