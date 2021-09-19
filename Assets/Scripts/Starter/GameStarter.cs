using Map;
using Spawner;
using TetriminoQueue;
using Zenject;

namespace Starter
{
	public class GameStarter : IInitializable
	{
		private readonly MapDrawer _mapDrawer;
		private readonly TetriminoQueueDrawer _queueDrawer;
		private readonly TetriminoSpawner _tetriminoSpawner;

		public GameStarter(MapDrawer mapDrawer,
			TetriminoSpawner tetriminoSpawner,
			TetriminoQueueDrawer queueDrawer)
		{
			_mapDrawer = mapDrawer;
			_tetriminoSpawner = tetriminoSpawner;
			_queueDrawer = queueDrawer;
		}

		public void Initialize()
		{
			_mapDrawer.Draw();
			_tetriminoSpawner.Spawn();
			_queueDrawer.Draw();
		}
	}
}