using System;
using Db.EnemyBehaviourDatabase;
using Enums;
using Gameplay.Enemy.Impls.Behaviours;

namespace Gameplay.Enemy.Impls
{
    public class EnemyBehaviourFactory: IEnemyBehaviourFactory
    {
        private readonly IEnemyBehaviorDatabase _enemyBehaviorDatabase;


        public EnemyBehaviourFactory(IEnemyBehaviorDatabase enemyBehaviorDatabase)
        {
            _enemyBehaviorDatabase = enemyBehaviorDatabase;
        }

        public IEnemyBehaviour Create(EBehaviourType type)
        {

            switch (type)
            {
                case EBehaviourType.Idle:
                {
                    return new IdleBehavior();
                }

                case EBehaviourType.SineWave:
                {
                    return new SineWaveBehavior(_enemyBehaviorDatabase.SineWaveAmplitude,
                        _enemyBehaviorDatabase.SineWaveFrequency, _enemyBehaviorDatabase.SineWaveSpeed);
                }

                default:
                    return new IdleBehavior();
            }
        }
    }
}