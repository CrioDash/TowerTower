using Enums;
using UnityEngine;

namespace Gameplay.Enemy
{
    public interface IEnemyFactory
    {
        EnemyBase Create(Vector3 position, EBehaviourType behaviourType, EEnemyType enemyType);
    }
}