using System;
using Config;
using Data;
using Extensions;
using UnityEngine;
using Zenject;

namespace DropPositionHighlighting
{
	public class DropCellView : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer _spriteRenderer;
		private MapConfig _mapConfig;

		[Inject]
		private void Construct(MapConfig mapConfig)
		{
			_mapConfig = mapConfig;
		}

		private void Start()
		{
			var size = _mapConfig.CellSize;
			_spriteRenderer.size = new Vector2(size, size);
		}

		public void Draw(CellPosition dropPosition)
		{
			transform.position = dropPosition.GetCellWorldPosition(_mapConfig);
		}

		public class Factory : PlaceholderFactory<DropCellView>
		{
		}
	}
}