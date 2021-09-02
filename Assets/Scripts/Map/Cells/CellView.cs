using Config;
using UnityEngine;
using Zenject;

namespace Map.Cells
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private MapConfig _mapConfig;
        
        [Inject]
        private void Construct(MapConfig mapConfig)
        {
            _mapConfig = mapConfig;
        }
        
        public void Draw()
        {
            var size = _mapConfig.CellSize;
            _spriteRenderer.size = new Vector2(size, size);
        }
        
        public class Factory : PlaceholderFactory<CellView>
        {
            
        }
    }
}