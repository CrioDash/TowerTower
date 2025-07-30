using System;
using Enums;
using UnityEngine;
using Zenject;

namespace Gameplay.Enemy.Impls
{
    public class PooledEnemyFactory : IEnemyFactory, IDisposable
    {
        private readonly EnemyBase.EnemyPool _enemyPool;
        private readonly EnemyBehaviourFactory _behaviourFactory;

        [Inject]
        public PooledEnemyFactory(EnemyBase.EnemyPool enemyPool,
            EnemyBehaviourFactory behaviourFactory
        )
        {
            _enemyPool = enemyPool;
            _behaviourFactory = behaviourFactory;
        }

        public EnemyBase Create(Vector3 position, EBehaviourType behaviourType, EEnemyType enemyType)
        {
            var enemy = _enemyPool.Spawn(position, _behaviourFactory.Create(behaviourType), enemyType);
            return enemy;
        }

        public void Dispose() {
            _enemyPool.Clear();
        }
    }
}