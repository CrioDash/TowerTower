using Enums;
using UnityEngine;

namespace Db.EnemySpawnDatabase.Impls
{
    [CreateAssetMenu(menuName = "Databases/EnemySpawnDatabase", fileName = "EnemySpawnDatabase")]
    public class EnemySpawnDatabase: ScriptableObject, IEnemySpawnDatabase
    {
        [SerializeField] private float spawnInterval;
        [SerializeField] private EEnemyType[] enemyTypes;

        public float SpawnInterval => spawnInterval;
        public EEnemyType[] EnemyTypes => enemyTypes;
    }
}