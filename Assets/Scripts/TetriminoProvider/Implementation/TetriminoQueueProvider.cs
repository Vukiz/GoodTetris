using Tetrimino.Data;
using TetriminoProvider.Infrastructure;
using TetriminoQueue;

namespace TetriminoProvider.Implementation
{
	public class TetriminoQueueProvider
	{
		private readonly ITetriminoesProvider _tetriminoesProvider;
		private readonly TetriminoQueueContainer _queueContainer;

		public TetriminoQueueProvider(ITetriminoesProvider tetriminoesProvider, TetriminoQueueContainer queueContainer)
		{
			_tetriminoesProvider = tetriminoesProvider;
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
				_queueContainer.AddPiece(_tetriminoesProvider.GetPiece());
			}
		}
	}
}