using Config;
using UnityEngine;
using Zenject;

namespace Starter.Installers.SOInstallers
{
	[CreateAssetMenu(fileName = "TetriminoQueueViewConfig", menuName = "Installers/TetriminoQueueViewConfig")]
	public class TetriminoQueueViewConfigInstaller : ScriptableObjectInstaller<TetriminoQueueViewConfigInstaller>
	{
		[SerializeField] private TetriminoQueueViewConfig _tetriminoQueueViewConfig;

		public override void InstallBindings()
		{
			Container.BindInstance(_tetriminoQueueViewConfig);
		}
	}
}