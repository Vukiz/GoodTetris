using System;
using System.Collections.Generic;
using Config;
using Tetrimino.Data;

namespace TetriminoQueue
{
	public class TetriminoQueueContainer
	{
		private readonly int _queueSize;
		public event Action PiecesUpdated;

		public TetriminoQueueContainer(TetriminoQueueViewConfig tetriminoQueueViewConfig)
		{
			_queueSize = tetriminoQueueViewConfig.HowManyTetriminoesToShow;
		}
		public Queue<TetriminoType> TetriminoQueue { get; } = new Queue<TetriminoType>();

		public bool IsQueueFull => TetriminoQueue.Count >= _queueSize;

		public void PiecesUpdatedInvoke() => PiecesUpdated?.Invoke();

		public void AddPiece(TetriminoType tetriminoType)
		{
			TetriminoQueue.Enqueue(tetriminoType);
		}
	}
}