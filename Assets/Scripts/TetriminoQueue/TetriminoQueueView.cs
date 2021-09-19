using UnityEngine;
using Zenject;

namespace TetriminoQueue
{
	public class TetriminoQueueView : MonoBehaviour
	{
		public void Init()
		{
			
		}
		
		public class Factory : PlaceholderFactory<TetriminoQueueView>
		{
		}
	}
}