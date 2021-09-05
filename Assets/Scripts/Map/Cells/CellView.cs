using Config;
using Data;
using TMPro;
using UnityEngine;
using Zenject;

namespace Map.Cells
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private GameObject _partDebugFilled;
        [SerializeField] private TMP_Text _text;
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

        public void SetPartDebugActive(bool isPartNotNull)
        {
            _partDebugFilled.SetActive(isPartNotNull);
        }

        public void SetText()
        {
            _text.SetText("");
        }
        public void SetText(CellPosition cellPosition)
        {
            _text.SetText($"{cellPosition.X} | {cellPosition.Y}");
        }
    }
}