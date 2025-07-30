using Enums;
using Models.Enemy;

namespace Db.EnemySettingsDatabase
{
    public interface IEnemySettingsDatabase
    { 
        public EnemySettingsVo[] EnemySettingsVos { get;}

        public EnemySettingsVo GetByType(EEnemyType type);
    }
}