using Config;
using CurrentTetriminoManager;
using Data;
using Game;
using Spawner.Infrastructure;
using TetriminoProvider.Infrastructure;
using Zenject;

namespace Spawner
{
	public class TetriminoSpawner : IInitializable
	{
		private readonly TetriminoManager _tetriminoManager;
		private readonly GameLineChecker _gameLineChecker;
		private readonly MapConfig _mapConfig;
		private readonly ITetriminoesProvider _tetriminoesProvider;
		private readonly ITetriminoFactory _tetriminoFactory;

		public TetriminoSpawner(GameLineChecker gameLineChecker,
			MapConfig mapConfig,
			ITetriminoesProvider tetriminoesProvider,
			ITetriminoFactory tetriminoFactory, 
			TetriminoManager tetriminoManager)
		{
			_gameLineChecker = gameLineChecker;
			_mapConfig = mapConfig;
			_tetriminoesProvider = tetriminoesProvider;
			_tetriminoFactory = tetriminoFactory;
			_tetriminoManager = tetriminoManager;
		}

		public void Initialize()
		{
			_gameLineChecker.LinesChecked += OnLinesChecked;
		}

		public void Spawn()
		{
			var tetriminoPiece = _tetriminoesProvider.GetPiece();
			var newTetriminoPosition = GetTetriminoPosition();
			var tetrimino = _tetriminoFactory.CreateTetrimino(tetriminoPiece, newTetriminoPosition);
			_tetriminoManager.CurrentTetrimino = tetrimino;
		}

		private CellPosition GetTetriminoPosition()
		{
			return new CellPosition(_mapConfig.MapWidth / 2, _mapConfig.MapHeight - 2);
		}

		private void OnLinesChecked()
		{
			Spawn();
		}
	}
}