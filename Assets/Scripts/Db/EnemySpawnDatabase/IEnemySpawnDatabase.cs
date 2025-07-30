using Enums;

namespace Db.EnemySpawnDatabase
{
    public interface IEnemySpawnDatabase
    {
        public float SpawnInterval { get; }
        public EEnemyType[] EnemyTypes { get; }
    }
}