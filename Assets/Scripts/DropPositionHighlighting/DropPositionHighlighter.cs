using System;
using System.Collections.Generic;
using System.Linq;
using CurrentTetriminoManager;
using Extensions;
using Map.Cells;
using Spawner;
using TetriminoMoving;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace DropPositionHighlighting
{
	public class DropPositionHighlighter : IInitializable, IDisposable
	{
		private readonly TetriminoManager _tetriminoManager;
		private readonly TetriminoMover _tetriminoMover;
		private readonly DropCellView.Factory _cellViewFactory;
		private readonly TetriminoSpawner _tetriminoSpawner;

		private readonly List<DropCellView> _drawnCells = new List<DropCellView>();

		public DropPositionHighlighter(TetriminoManager tetriminoManager, TetriminoMover tetriminoMover,
			DropCellView.Factory cellViewFactory, TetriminoSpawner tetriminoSpawner)
		{
			_tetriminoManager = tetriminoManager;
			_tetriminoMover = tetriminoMover;
			_cellViewFactory = cellViewFactory;
			_tetriminoSpawner = tetriminoSpawner;
		}

		public void Initialize()
		{
			_tetriminoMover.TetriminoMoved += OnTetriminoMoved;
			_tetriminoSpawner.TetriminoSpawned += OnTetriminoMoved;

			InitDropCells();
		}

		private void InitDropCells()
		{
			for (var i = 0; i < 4; i++)
			{
				var cellView = _cellViewFactory.Create();
				_drawnCells.Add(cellView);
			}

			OnTetriminoMoved();
		}

		public void Dispose()
		{
			_tetriminoMover.TetriminoMoved -= OnTetriminoMoved;
			_tetriminoSpawner.TetriminoSpawned -= OnTetriminoMoved;
			foreach (var dropCellView in _drawnCells)
			{
				if (dropCellView != null)
				{
					Object.Destroy(dropCellView.gameObject);
				}
			}

			_drawnCells.Clear();
		}

		private void OnTetriminoMoved()
		{
			var tetrimino = _tetriminoManager.CurrentTetrimino;
			var tetriminoWorldPosition = tetrimino.Model.TetriminoPosition;
			var dropPosition = _tetriminoMover.GetDropPosition(tetrimino, ref tetriminoWorldPosition).ToList();
			
			DrawDropPhantom(dropPosition.ToList());
		}

		private void DrawDropPhantom(IEnumerable<CellMoveData> dropPosition)
		{
			var dropPositions = dropPosition.Select(p => p.PositionTransformation.NewPosition).ToList();
			for (var i = 0; i < dropPositions.Count; i++)
			{
				_drawnCells[i].Draw(dropPositions[i]);
			}
		}
	}
}