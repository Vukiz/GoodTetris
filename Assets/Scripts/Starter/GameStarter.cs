using Map;
using Spawner;
using Zenject;

namespace Starter
{
	public class GameStarter : IInitializable
	{
		private readonly MapDrawer _mapDrawer;
		private readonly TetriminoSpawner _tetriminoSpawner;

		public GameStarter(MapDrawer mapDrawer,
			TetriminoSpawner tetriminoSpawner)
		{
			_mapDrawer = mapDrawer;
			_tetriminoSpawner = tetriminoSpawner;
		}

		public void Initialize()
		{
			_mapDrawer.Draw();
			_tetriminoSpawner.Spawn();
		}
	}
}