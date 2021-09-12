using System;
using System.Collections.Generic;
using System.Linq;
using CurrentTetriminoManager;
using Extensions;
using Map;
using UnityEngine;
using Zenject;

namespace Game
{
	public class GameLineChecker : IInitializable, IDisposable
	{
		private readonly MapDataModel _mapDataModel;
		private readonly TetriminoManager _tetriminoManager;

		public event Action LinesChecked;

		public GameLineChecker(MapDataModel mapDataModel, TetriminoManager tetriminoManager)
		{
			_mapDataModel = mapDataModel;
			_tetriminoManager = tetriminoManager;
		}

		public void Initialize()
		{
			_tetriminoManager.TetriminoDown += OnTetriminoDown;
		}

		public void Dispose()
		{
			_tetriminoManager.TetriminoDown -= OnTetriminoDown;
		}

		private void OnTetriminoDown()
		{
			CheckLines();
		}

		private void CheckLines()
		{
			var filledRows = new List<int>();
			for (var rowIndex = 0; rowIndex < _mapDataModel.Grid.Count; rowIndex++)
			{
				if (_mapDataModel.Grid[rowIndex].IsRowFilled())
				{
					filledRows.Add(rowIndex);
				}
			}

			if (filledRows.Any())
			{
				_mapDataModel.ClearFilledLines(filledRows);
				_mapDataModel.CollapseEmptyLines();
			}
			
			LinesChecked?.Invoke();
		}
	}
}