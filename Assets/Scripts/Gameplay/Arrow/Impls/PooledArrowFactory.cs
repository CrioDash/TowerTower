using System;
using UnityEngine;
using Zenject;

namespace Gameplay.Arrow.Impls
{
    public class PooledArrowFactory: IArrowFactory, IDisposable
    {
        private readonly Transform _arrowContainer;
        private readonly Arrow.ArrowPool _arrowPool;

        public Transform ArrowContainer => _arrowContainer;
        
        public PooledArrowFactory(
            Transform arrowContainer,
            Arrow.ArrowPool pool)
        {
            _arrowContainer = arrowContainer;
            _arrowPool = pool;
        }

        public Arrow Create(Vector3 position, Vector3 angle, Vector3 force) {
            return _arrowPool.Spawn(position, angle, force);
        }

        public void Dispose() {
            _arrowPool.Clear();
        }
        
    }
}