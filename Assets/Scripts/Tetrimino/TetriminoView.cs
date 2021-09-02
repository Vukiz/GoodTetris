using System;
using System.Collections.Generic;
using Config;
using Data;
using Extensions;
using UnityEngine;
using Zenject;

namespace Tetrimino
{
    public class TetriminoView : MonoBehaviour, IDisposable
    {
        private TetriminoDataModel _tetriminoDataModel;
        private TetriminoPartView.Factory _tetriminoPartFactory;

        private MapConfig _mapConfig;

        private readonly List<TetriminoPartView> _parts = new List<TetriminoPartView>();

        [Inject]
        public void Construct(TetriminoPartView.Factory tetriminoPartFactory,
            MapConfig mapConfig)
        {
            _tetriminoPartFactory = tetriminoPartFactory;
            _mapConfig = mapConfig;
        }

        public void Init(TetriminoDataModel tetriminoDataModel)
        {
            _tetriminoDataModel = tetriminoDataModel;
            Draw();
        }

        public void UpdateRotatedParts()
        {
            ClearParts();
            Draw();
        }

        private void Draw()
        {
            var parts = _tetriminoDataModel.RotatedParts;
            var cellSize = _mapConfig.CellSize;
            transform.position = _tetriminoDataModel.TetriminoPosition.GetCellWorldPosition(_mapConfig);
            CreatePart(_tetriminoDataModel.TetriminoCenter, cellSize);
            foreach (var partsPosition in parts.PartsPositions)
            {
                CreatePart(partsPosition, cellSize);
            }
        }

        private void CreatePart(CellPosition partPosition, float cellSize)
        {
            var tetriminoPart = _tetriminoPartFactory.Create();
            tetriminoPart.transform.SetParent(transform);
            tetriminoPart.transform.localPosition = partPosition.GetCellWorldPosition(_mapConfig);
            tetriminoPart.SetSize(cellSize);
            _parts.Add(tetriminoPart);
        }

        public void Dispose()
        {
        }

        private void ClearParts()
        {
            foreach (var partView in _parts)
            {
                Destroy(partView);
            }

            _parts.Clear();
        }

        public class Factory : PlaceholderFactory<TetriminoView>
        {
        }
    }
}