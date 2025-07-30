using System;
using Db.EnemySettingsDatabase;
using Db.EnemySpawnDatabase;
using Enums;
using Extensions;
using Gameplay.Enemy.Impls;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core
{
    public class EnemySpawner: MonoBehaviour
    {
        [SerializeField] private Transform[] airSpawnPoints;
        [SerializeField] private Transform[] groundSpawnPoints;

        private IEnemySpawnDatabase _enemySpawnDatabase;
        private IEnemySettingsDatabase _enemySettingsDatabase;
        private PooledEnemyFactory _enemyFactory;

        [Inject]
        private void Construct(IEnemySpawnDatabase enemySpawnDatabase,
            IEnemySettingsDatabase enemySettingsDatabase,
            PooledEnemyFactory enemyFactory)
        {
            _enemySpawnDatabase = enemySpawnDatabase;
            _enemySettingsDatabase = enemySettingsDatabase;
            _enemyFactory = enemyFactory;
        }

        private void Start()
        {
            Observable.Interval(TimeSpan.FromSeconds(_enemySpawnDatabase.SpawnInterval)).Subscribe(_ => SpawnEnemy()).AddTo(this);
        }

        private void SpawnEnemy()
        {
            var enemyStats = _enemySettingsDatabase.EnemySettingsVos.PickRandom();

            Vector3 pos = enemyStats.movementType == EMovementType.Ground ? groundSpawnPoints.PickRandom().position : airSpawnPoints.PickRandom().position ;
            
            var enemy = _enemyFactory.Create(pos, enemyStats.behaviourType, enemyStats.enemyType);
        }
    }
}