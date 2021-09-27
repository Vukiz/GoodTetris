using System;
using System.Collections.Generic;
using System.Linq;
using Config;
using Data;
using Extensions;
using Spawner;
using UnityEngine;
using Zenject;

namespace Tetrimino
{
	public class TetriminoView : MonoBehaviour, IDisposable
	{
		private TetriminoDataModel _tetriminoDataModel;

		private MapConfig _mapConfig;
		private TetriminoesConfig _tetriminoesConfig;
		private TetriminoPartCreator _partCreator;

		[Inject]
		public void Construct(
			TetriminoesConfig tetriminoesConfig,
			MapConfig mapConfig,
			TetriminoPartCreator partCreator)
		{
			_mapConfig = mapConfig;
			_tetriminoesConfig = tetriminoesConfig;
			_partCreator = partCreator;
		}

		public void Init(TetriminoDataModel tetriminoDataModel)
		{
			_tetriminoDataModel = tetriminoDataModel;
		}

		public IEnumerable<TetriminoPartView> CreateParts()
		{
			var parts = _tetriminoesConfig.GetPartsPositions(_tetriminoDataModel.TetriminoType);
			var cellSize = _mapConfig.CellSize;
			transform.position = _tetriminoDataModel.TetriminoPosition.GetCellWorldPosition(_mapConfig);
			yield return CreatePart(TetriminoDataModel.TetriminoCenter, cellSize);
			foreach (var partsPosition in parts)
			{
				yield return CreatePart(partsPosition, cellSize);
			}
		}

		private TetriminoPartView CreatePart(CellPosition partPosition, float cellSize)
		{
			var tetriminoPart = _partCreator.CreatePart(transform, partPosition, cellSize);
			tetriminoPart.PartCleared += OnPartCleared;

			return tetriminoPart;
		}

		private void OnPartCleared(TetriminoPartView tetriminoPartView)
		{
			_tetriminoDataModel.PartsHolder.RemovePart(tetriminoPartView);
			if (!_tetriminoDataModel.PartsHolder.Parts.Any())
			{
				Destroy(gameObject);
			}
		}

		public void Dispose()
		{
			ClearParts();
		}

		private void ClearParts()
		{
			var parts = _tetriminoDataModel.PartsHolder;
			foreach (var partView in parts.Parts)
			{
				partView.PartCleared -= OnPartCleared;
				Destroy(partView.gameObject);
			}

			_tetriminoDataModel.PartsHolder.Parts.Clear();
		}

		public class Factory : PlaceholderFactory<TetriminoView>
		{
		}
	}
}