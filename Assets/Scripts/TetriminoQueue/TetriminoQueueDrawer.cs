using System;
using System.Collections.Generic;
using Spawner.Infrastructure;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace TetriminoQueue
{
	public class TetriminoQueueDrawer : IInitializable, IDisposable
	{
		private List<TetriminoQueueView> _tetriminoQueueViews = new List<TetriminoQueueView>();
		private readonly TetriminoQueueContainer _queueContainer;
		private readonly ITetriminoFactory _tetriminoFactory;

		public TetriminoQueueDrawer(TetriminoQueueContainer queueContainer,
			ITetriminoFactory tetriminoFactory
		)
		{
			_queueContainer = queueContainer;
			_tetriminoFactory = tetriminoFactory;
		}

		public void Initialize()
		{
			_queueContainer.PiecesUpdated += OnPiecesUpdated;
			CreateViews();
		}

		private void CreateViews()
		{
			foreach (var tetriminoType in _queueContainer.TetriminoQueue)
			{
				
			}
		}

		public void Dispose()
		{
			_queueContainer.PiecesUpdated -= OnPiecesUpdated;
			ClearViews();
		}

		private void ClearViews()
		{
			foreach (var tetriminoQueueView in _tetriminoQueueViews)
			{
				Object.Destroy(tetriminoQueueView.gameObject);
			}

			_tetriminoQueueViews.Clear();
		}

		private void OnPiecesUpdated()
		{
			Draw();
		}

		public void Draw()
		{
		}
	}
}