using Config;
using UnityEngine;
using Zenject;

namespace Starter.Installers
{
    [CreateAssetMenu(fileName = "MapSettingsInstaller", menuName = "Installers/MapSettingsInstaller")]
    public class MapSettingsInstaller : ScriptableObjectInstaller<MapSettingsInstaller>
    {
        [SerializeField] private MapConfig _mapConfig;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_mapConfig);
        }
    }
}