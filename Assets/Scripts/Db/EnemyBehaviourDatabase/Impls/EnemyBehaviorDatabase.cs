using UnityEngine;

namespace Db.EnemyBehaviourDatabase.Impls
{
    [CreateAssetMenu(menuName = "Databases/EnemyBehaviorDatabase", fileName = "EnemyBehaviorDatabase")]
    public class EnemyBehaviorDatabase: ScriptableObject, IEnemyBehaviorDatabase
    {
        [Header("SineWave Behavior")] 
        [SerializeField] private float sineWaveAmplitude;
        [SerializeField] private float sineWaveFrequency;
        [SerializeField] private float sineWaveSpeed;

        public float SineWaveAmplitude => sineWaveAmplitude;
        public float SineWaveFrequency => sineWaveFrequency;
        public float SineWaveSpeed => sineWaveSpeed;
    }
}