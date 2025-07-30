using System.Linq;
using Enums;
using Models.Enemy;
using UnityEngine;

namespace Db.EnemySettingsDatabase.Impls
{
    [CreateAssetMenu(menuName = "Databases/EnemySettingsDatabase", fileName = "EnemySettingsDatabase")]
    public class EnemySettingsDatabase: ScriptableObject, IEnemySettingsDatabase
    {
        [SerializeField] private EnemySettingsVo[] enemySettingsVos;

        public EnemySettingsVo[] EnemySettingsVos => enemySettingsVos;

        public EnemySettingsVo GetByType(EEnemyType type)
        {
            return enemySettingsVos.Single(x => type == x.enemyType);
        }
    }
}