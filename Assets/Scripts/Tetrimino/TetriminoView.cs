using System;
using System.Linq;
using Config;
using Data;
using Extensions;
using Map;
using UnityEngine;
using Zenject;

namespace Tetrimino
{
	public class TetriminoView : MonoBehaviour, IDisposable
	{
		private TetriminoDataModel _tetriminoDataModel;
		private TetriminoPartView.Factory _tetriminoPartFactory;

		private MapConfig _mapConfig;
		private MapDataModel _mapDataModel;

		[Inject]
		public void Construct(TetriminoPartView.Factory tetriminoPartFactory,
			MapConfig mapConfig, MapDataModel mapDataModel)
		{
			_tetriminoPartFactory = tetriminoPartFactory;
			_mapConfig = mapConfig;
			_mapDataModel = mapDataModel;
		}

		public void Init(TetriminoDataModel tetriminoDataModel)
		{
			_tetriminoDataModel = tetriminoDataModel;
			CreateParts();
		}

		private void CreateParts()
		{
			var parts = _tetriminoDataModel.PartsHolder.PartsPositions.ToList();
			var cellSize = _mapConfig.CellSize;
			transform.position = _tetriminoDataModel.TetriminoPosition.GetCellWorldPosition(_mapConfig);
			CreatePart(_tetriminoDataModel.TetriminoCenter, cellSize);
			foreach (var partsPosition in parts)
			{
				CreatePart(partsPosition, cellSize);
			}
		}

		private void CreatePart(CellPosition partPosition, float cellSize)
		{
			var tetriminoPart = _tetriminoPartFactory.Create();
			tetriminoPart.transform.SetParent(transform);
			tetriminoPart.transform.localPosition = partPosition.GetCellWorldPosition(_mapConfig);
			tetriminoPart.SetSize(cellSize);
			tetriminoPart.PartCleared += OnPartCleared;

			_tetriminoDataModel.PartsHolder.SetPart(partPosition, tetriminoPart);
			_mapDataModel.SetTetriminoPartViewToCell(partPosition + _tetriminoDataModel.TetriminoPosition, tetriminoPart);
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
			foreach (var partView in parts.Parts.Values)
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