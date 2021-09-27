using System;
using System.Collections.Generic;
using System.Linq;
using Config;
using Data;
using Spawner.Infrastructure;
using Tetrimino;
using Zenject;
using Object = UnityEngine.Object;

namespace TetriminoQueue
{
	public class TetriminoQueueDrawer : IInitializable, IDisposable
	{
		private readonly List<TetriminoHolder> _tetriminoHolders = new List<TetriminoHolder>();
		private readonly TetriminoQueueContainer _queueContainer;
		private readonly ITetriminoFactory _tetriminoFactory;
		private readonly TetriminoQueueViewConfig _tetriminoQueueViewConfig;

		public TetriminoQueueDrawer(
			TetriminoQueueContainer queueContainer,
			ITetriminoFactory tetriminoFactory,
			TetriminoQueueViewConfig tetriminoQueueViewConfig)
		{
			_queueContainer = queueContainer;
			_tetriminoFactory = tetriminoFactory;
			_tetriminoQueueViewConfig = tetriminoQueueViewConfig;
		}

		public void Initialize()
		{
			_queueContainer.PiecesUpdated += OnPiecesUpdated;
			CreateViews();
		}

		public void Dispose()
		{
			_queueContainer.PiecesUpdated -= OnPiecesUpdated;
			ClearViews();
		}

		private void CreateViews()
		{
			var tetriminoTypes = _queueContainer.TetriminoQueue.ToList();
			for (var queueViewIndex = 0; queueViewIndex < _queueContainer.TetriminoQueue.Count; queueViewIndex++)
			{
				var tetriminoType = tetriminoTypes[queueViewIndex];
				var tetriminoHolder =
					_tetriminoFactory.CreateTetrimino(tetriminoType, GetPositionByIndex(queueViewIndex));
				_tetriminoHolders.Add(tetriminoHolder);
			}
		}

		private CellPosition GetPositionByIndex(int queueViewIndex)
		{
			return _tetriminoQueueViewConfig.StartPosition + _tetriminoQueueViewConfig.Spacing * queueViewIndex;
		}

		private void ClearViews()
		{
			foreach (var tetriminoQueueView in _tetriminoHolders)
			{
				if (tetriminoQueueView?.View != null)
				{
					Object.Destroy(tetriminoQueueView.View.gameObject);
				}
			}

			_tetriminoHolders.Clear();
		}

		private void OnPiecesUpdated()
		{
			ClearViews();
			CreateViews();
		}

		public void Draw()
		{
			OnPiecesUpdated();
		}
	}
}