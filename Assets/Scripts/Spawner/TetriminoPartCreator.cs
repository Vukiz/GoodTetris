using Data;
using Tetrimino;
using UnityEngine;

namespace Spawner
{
	public class TetriminoPartCreator
	{
		private readonly TetriminoPartView.Factory _tetriminoPartFactory;

		public TetriminoPartCreator(TetriminoPartView.Factory tetriminoPartFactory)
		{
			_tetriminoPartFactory = tetriminoPartFactory;
		}

		public TetriminoPartView CreatePart(Transform parent, CellPosition partPosition, float cellSize)
		{
			var tetriminoPart = _tetriminoPartFactory.Create();
			tetriminoPart.transform.SetParent(parent);
			tetriminoPart.SetLocalPosition(partPosition);
			tetriminoPart.SetSize(cellSize);
			return tetriminoPart;
		}
	}
}