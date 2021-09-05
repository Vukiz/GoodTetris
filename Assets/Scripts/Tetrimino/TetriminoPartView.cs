using System;
using Config;
using Data;
using Extensions;
using UnityEngine;
using Zenject;

namespace Tetrimino
{
	public class TetriminoPartView : MonoBehaviour
	{
		public Action<TetriminoPartView> PartCleared;
		[SerializeField] private SpriteRenderer _spriteRenderer;

		private MapConfig _mapConfig;

		[Inject]
		private void Construct(MapConfig mapConfig)
		{
			_mapConfig = mapConfig;
		}

		public void SetSize(float newSize)
		{
			_spriteRenderer.size = new Vector2(newSize, newSize);
		}

		public void Clear()
		{
			PartCleared.Invoke(this);
			Destroy(gameObject);
		}

		public class Factory : PlaceholderFactory<TetriminoPartView>
		{
		}

		public void MoveToNewPosition(CellPosition lowerCellCellPosition)
		{
			transform.position = lowerCellCellPosition.GetCellWorldPosition(_mapConfig);
		}
	}
}