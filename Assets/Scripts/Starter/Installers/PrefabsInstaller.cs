using Config;
using UnityEngine;
using Zenject;

namespace Starter.Installers
{
    [CreateAssetMenu(fileName = "PrefabsInstaller", menuName = "Installers/PrefabsInstaller")]
    public class PrefabsInstaller: ScriptableObjectInstaller<PrefabsInstaller>
    {
        [SerializeField] private PrefabsConfig _prefabsConfig;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_prefabsConfig);
        }
    }
}