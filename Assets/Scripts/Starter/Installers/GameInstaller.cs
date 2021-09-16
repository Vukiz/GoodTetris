using Config;
using DropPositionHighlighting;
using Game;
using Map;
using Map.Cells;
using Player;
using Tetrimino;
using TetriminoProvider.Implementation;
using TetriminoProvider.Infrastructure;
using Zenject;

namespace Starter.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Inject] private PrefabsConfig _prefabsConfig = null;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameTicker>().AsSingle();
            Container.Bind<ITetriminoesProvider>().To<BagRandomizer>().AsTransient();
            InstallMap();
            InstallTetriminoes();
            InstallPlayer();
            TetriminoInstaller.Install(Container);
            LevelInstaller.Install(Container);
            Container.BindInterfacesTo<GameStarter>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameLineChecker>().AsSingle();
            Container.BindInterfacesTo<DropPositionHighlighter>().AsSingle();
        }

        private void InstallPlayer()
        {           
            Container.Bind<PlayerInputController>().FromComponentInNewPrefab(_prefabsConfig.PlayerInputController).AsSingle();
            Container.BindInterfacesTo<PlayerTetriminoMover>().AsSingle();
            Container.BindInterfacesTo<PlayerTetriminoRotator>().AsSingle();
        }

        private void InstallMap()
        {
            Container.BindInterfacesAndSelfTo<MapDataModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<MapDrawer>().AsTransient();
            Container.BindFactory<CellView, CellView.Factory>()
                .FromComponentInNewPrefab(_prefabsConfig.CellPrefab)
                .UnderTransformGroup("Grid");
            Container.BindFactory<DropCellView, DropCellView.Factory>()
                .FromComponentInNewPrefab(_prefabsConfig.DropCellPrefab)
                .UnderTransformGroup("Grid");
        }

        private void InstallTetriminoes()
        {
            Container.Bind<TetriminoDataModel>().AsTransient();
            Container.BindFactory<TetriminoView, TetriminoView.Factory>()
                .FromComponentInNewPrefab(_prefabsConfig.TetriminoView)
                .UnderTransformGroup("Tetriminoes");
            Container.BindFactory<TetriminoPartView, TetriminoPartView.Factory>()
                .FromComponentInNewPrefab(_prefabsConfig.TetriminoPart)
                .UnderTransformGroup("Tetriminoes");
        }
    }
}