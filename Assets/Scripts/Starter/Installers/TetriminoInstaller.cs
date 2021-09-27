using Config;
using CurrentTetriminoManager;
using Spawner;
using Spawner.Implementation;
using Spawner.Infrastructure;
using Tetrimino;
using TetriminoMoving;
using Zenject;

namespace Starter.Installers
{
	public class TetriminoInstaller : Installer<TetriminoInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<ITetriminoFactory>().To<TetriminoFactory>().AsSingle();
			Container.Bind<TetriminoManager>().AsSingle();
			Container.Bind<TetriminoMover>().AsSingle();
			Container.Bind<TetriminoPartCreator>().AsSingle();
			Container.BindInterfacesAndSelfTo<TetriminoSpawner>().AsSingle();
			Container.BindInterfacesTo<GameTickTetriminoMover>().AsSingle();
		}

		public static void InstallTetriminoes(DiContainer container, PrefabsConfig prefabsConfig)
		{
			container.Bind<TetriminoDataModel>().AsTransient();
			container.BindFactory<TetriminoView, TetriminoView.Factory>()
				.FromComponentInNewPrefab(prefabsConfig.TetriminoView)
				.UnderTransformGroup("Tetriminoes");
			container.BindFactory<TetriminoPartView, TetriminoPartView.Factory>()
				.FromComponentInNewPrefab(prefabsConfig.TetriminoPart)
				.UnderTransformGroup("Tetriminoes");
		}
	}
}