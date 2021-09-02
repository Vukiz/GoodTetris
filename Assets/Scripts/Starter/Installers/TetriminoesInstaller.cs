using Config;
using UnityEngine;
using Zenject;

namespace Starter.Installers
{
    [CreateAssetMenu(fileName = "TetriminoesInstaller", menuName = "Installers/TetriminoesInstaller")]
    public class TetriminoesInstaller : ScriptableObjectInstaller<TetriminoesInstaller>
    {
        [SerializeField] private TetriminoesConfig _tetriminoesConfig;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_tetriminoesConfig);
        }
    }
}