using System;
using Enums;
using UnityEngine.Serialization;

namespace Models.Enemy
{
    [Serializable]
    public class EnemySettingsVo
    {
        public EEnemyType enemyType;
        public int maxHealth;
        public EBehaviourType behaviourType;
        public EMovementType movementType;
    }
}