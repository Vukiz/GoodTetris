using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using Tetrimino;
using Tetrimino.Data;
using TetriminoProvider.Infrastructure;

namespace TetriminoProvider.Implementation
{
    public class BagRandomizer : ITetriminoesProvider
    {
        private Queue<TetriminoType> _bag;

        private void RefillBag()
        {
            var bagList = Enum.GetValues(typeof(TetriminoType)).Cast<TetriminoType>().ToList().Shuffle();
            _bag = new Queue<TetriminoType>(bagList);
        }

        public TetriminoType GetPiece()
        {
            if (_bag == null || !_bag.Any())
            {
                RefillBag();
            }
            
            return _bag.Dequeue();
        }
    }
}