using System;
using System.Collections.Generic;
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

		public void Init(TetriminoDataModel tetriminoDataModel, IEnumerable<CellPosition> parts)
		{
			_tetriminoDataModel = tetriminoDataModel;
			CreateParts(parts);
		}

		private void CreateParts(IEnumerable<CellPosition> parts)
		{
			var cellSize = _mapConfig.CellSize;
			transform.position = _tetriminoDataModel.TetriminoPosition.GetCellWorldPosition(_mapConfig);
			CreatePart(TetriminoDataModel.TetriminoCenter, cellSize);
			foreach (var partsPosition in parts)
			{
				CreatePart(partsPosition, cellSize);
			}
		}

		private void CreatePart(CellPosition partPosition, float cellSize)
		{
			var tetriminoPart = _tetriminoPartFactory.Create();
			tetriminoPart.transform.SetParent(transform);
			tetriminoPart.SetLocalPosition(partPosition);
			tetriminoPart.SetSize(cellSize);
			tetriminoPart.PartCleared += OnPartCleared;

			_tetriminoDataModel.PartsHolder.AddPart(tetriminoPart);
			_mapDataModel.SetTetriminoPartViewToCell(partPosition + _tetriminoDataModel.TetriminoPosition,
				tetriminoPart);
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