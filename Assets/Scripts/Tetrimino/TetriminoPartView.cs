using UnityEngine;
using Zenject;

namespace Tetrimino
{
    public class TetriminoPartView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void SetSize(float newSize)
        {
            _spriteRenderer.size = new Vector2(newSize, newSize);
        }
        
        public class Factory : PlaceholderFactory<TetriminoPartView>
        {
            
        }
    }
}