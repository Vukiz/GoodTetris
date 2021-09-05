using Config;
using CurrentTetriminoManager;
using Data;
using Spawner.Infrastructure;
using TetriminoProvider.Infrastructure;
using Zenject;

namespace Spawner
{
    public class TetriminoSpawner : IInitializable
    {
        private readonly TetriminoManager _tetriminoManager;
        private readonly MapConfig _mapConfig;
        private readonly ITetriminoesProvider _tetriminoesProvider;
        private readonly ITetriminoFactory _tetriminoFactory;

        public TetriminoSpawner(TetriminoManager tetriminoManager,
            MapConfig mapConfig,
            ITetriminoesProvider tetriminoesProvider,
            ITetriminoFactory tetriminoFactory)
        {
            _tetriminoManager = tetriminoManager;
            _mapConfig = mapConfig;
            _tetriminoesProvider = tetriminoesProvider;
            _tetriminoFactory = tetriminoFactory;
        }

        public void Initialize()
        {
            _tetriminoManager.TetriminoDown += OnTetriminoDown;
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

        private void OnTetriminoDown()
        {
            Spawn();
        }
    }
}