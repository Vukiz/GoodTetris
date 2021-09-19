using System;
using System.Collections.Generic;
using Tetrimino.Data;

namespace TetriminoQueue
{
	public class TetriminoQueueContainer
	{
		private const int QueueSize = 7;
		public event Action PiecesUpdated;

		public Queue<TetriminoType> TetriminoQueue { get; } = new Queue<TetriminoType>();

		public bool IsQueueFull => TetriminoQueue.Count >= QueueSize;

		public void PiecesUpdatedInvoke() => PiecesUpdated?.Invoke();

		public void AddPiece(TetriminoType tetriminoType)
		{
			TetriminoQueue.Enqueue(tetriminoType);
		}
	}
}