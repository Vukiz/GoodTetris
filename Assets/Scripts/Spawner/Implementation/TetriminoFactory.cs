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

		public TetriminoHolder CreateTetrimino(TetriminoType tetriminoType, CellPosition tetriminoPosition)
		{
			var view = _tetriminoFactory.Create();
			var tetriminoDataModel = new TetriminoDataModel
			{
				TetriminoType = tetriminoType,
				TetriminoPosition = tetriminoPosition,
				RotationPoint = _tetriminoesConfig.GetRotationPoint(tetriminoType)
			};

			view.Init(tetriminoDataModel);
			var parts = view.CreateParts();
			tetriminoDataModel.PartsHolder.AddParts(parts);

			var result = new TetriminoHolder
			{
				View = view,
				Model = tetriminoDataModel
			};

			return result;
		}
	}
}