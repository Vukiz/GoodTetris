using Tetrimino.Data;
using TetriminoProvider.Infrastructure;
using TetriminoQueue;

namespace TetriminoProvider.Implementation
{
	public class TetriminoQueueProvider : ITetriminoesProvider
	{
		private readonly BagRandomizer _bagRandomizer;
		private readonly TetriminoQueueContainer _queueContainer;


		public TetriminoQueueProvider(BagRandomizer bagRandomizer, TetriminoQueueContainer queueContainer)
		{
			_bagRandomizer = bagRandomizer;
			_queueContainer = queueContainer;
			RefillQueue();
		}

		public TetriminoType GetPiece()
		{
			var nextTetrimino = _queueContainer.TetriminoQueue.Dequeue();
			if (!_queueContainer.IsQueueFull)
			{
				RefillQueue();
			}
			
			_queueContainer.PiecesUpdatedInvoke();;

			return nextTetrimino;
		}

		private void RefillQueue()
		{
			while (!_queueContainer.IsQueueFull)
			{
				_queueContainer.AddPiece(_bagRandomizer.GetPiece());
			}
		}
	}
}