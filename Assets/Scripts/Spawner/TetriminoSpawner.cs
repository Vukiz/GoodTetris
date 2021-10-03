using System;
using Config;
using CurrentTetriminoManager;
using Data;
using Game;
using Map;
using Spawner.Infrastructure;
using TetriminoProvider.Implementation;
using Zenject;

namespace Spawner
{
	public class TetriminoSpawner : IInitializable
	{
		private readonly TetriminoManager _tetriminoManager;
		private readonly GameLineChecker _gameLineChecker;
		private readonly MapConfig _mapConfig;
		private readonly TetriminoQueueProvider _tetriminoesProvider;
		private readonly ITetriminoFactory _tetriminoFactory;
		private readonly MapDataModel _mapDataModel;

		public event Action TetriminoSpawned;

		public TetriminoSpawner(GameLineChecker gameLineChecker,
			MapConfig mapConfig,
			TetriminoQueueProvider tetriminoesProvider,
			ITetriminoFactory tetriminoFactory,
			TetriminoManager tetriminoManager, 
			MapDataModel mapDataModel)
		{
			_gameLineChecker = gameLineChecker;
			_mapConfig = mapConfig;
			_tetriminoesProvider = tetriminoesProvider;
			_tetriminoFactory = tetriminoFactory;
			_tetriminoManager = tetriminoManager;
			_mapDataModel = mapDataModel;
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

			_mapDataModel.SetTetriminoPartsToTheirInitialCells(tetrimino.TetriminoPartsCreationData);
			_tetriminoManager.CurrentTetrimino = tetrimino;
			TetriminoSpawned?.Invoke();
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