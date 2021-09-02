using CurrentTetriminoManager;
using Spawner;
using Spawner.Implementation;
using Spawner.Infrastructure;
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
            Container.BindInterfacesAndSelfTo<TetriminoSpawner>().AsSingle();
            Container.BindInterfacesTo<GameTickTetriminoMover>().AsSingle();
        }
    }
}