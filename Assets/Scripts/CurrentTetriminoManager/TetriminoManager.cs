using System;
using Tetrimino;
using Zenject;

namespace CurrentTetriminoManager
{
    public class TetriminoManager : IInitializable
    {
        public Action TetriminoDown;

        public TetriminoHolder CurrentTetrimino { get; set; }

        public void Initialize()
        {
        }

        public void TetriminoDownInvoke()
        {                
            CurrentTetrimino = null;

            TetriminoDown?.Invoke();
        }
    }
}