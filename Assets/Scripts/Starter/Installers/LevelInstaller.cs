using Leveling.Strategies;
using Zenject;

namespace Starter.Installers
{
    public class LevelInstaller : Installer<LevelInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<LevelManager.LevelManager>();
            Container.Bind<BaseLevelingStrategy>().To<FixedGoalLevelingStrategy>().AsTransient();
        }
    }
}