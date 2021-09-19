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
		public CellPosition WorldCellPosition { get; private set; }

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

		public void SetLocalPosition(CellPosition localPosition, MapConfig mapConfig = null)
		{
			transform.localPosition = new Vector3(localPosition.X, localPosition.Y, 0);
		}

		public void SetWorldPosition(CellPosition newCellPosition)
		{
			WorldCellPosition = newCellPosition;
			transform.position = newCellPosition.GetCellWorldPosition(_mapConfig);
		}

		public void Clear()
		{
			PartCleared.Invoke(this);
			Destroy(gameObject);
		}

		public class Factory : PlaceholderFactory<TetriminoPartView>
		{
		}
	}
}