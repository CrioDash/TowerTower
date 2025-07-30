using Db.EnemySettingsDatabase;
using Enums;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable, IPoolable<Vector3, IEnemyBehaviour, EEnemyType>
    {
        private readonly ReactiveProperty<int> _health = new ReactiveProperty<int>();
        public IReadOnlyReactiveProperty<int> Health => _health; 
        
        private readonly ReactiveProperty<IEnemyBehaviour> _behaviour = new ReactiveProperty<IEnemyBehaviour>();
        public IReadOnlyReactiveProperty<IEnemyBehaviour> Behaviour => _behaviour; 
        
        private EnemyPool _enemyPool;
        private IEnemySettingsDatabase _enemySettingsDatabase;
        private EMovementType _movementType;
        private EEnemyType _type;

        [Inject]
        private void Construct(
            EnemyPool enemyPool,
            IEnemySettingsDatabase enemySettingsDatabase
            )
        {
            _enemyPool = enemyPool;
            _enemySettingsDatabase = enemySettingsDatabase;
        }
        
        private void Awake()
        {
            SetStats();
        }

        private void Update()
        {
            _behaviour.Value.Tick(Time.deltaTime);
        }

        public void TakeDamage(int amount)
        {
            if (amount <= 0 || _health.Value == 0) return;

            _health.Value = Mathf.Max(_health.Value - amount, 0);
            
            if (_health.Value == 0)
                Die();
        }
        
        public void OnSpawned(Vector3 position, IEnemyBehaviour behaviour, EEnemyType type)
        {
            transform.position = position;
            _type = type;
            _behaviour.Value = behaviour;
            _behaviour.Value.Initialize(this);
            
            SetStats();
        }

        public void OnDespawned()
        {
            gameObject.SetActive(false);
            var stickedArrows = GetComponentsInChildren<Arrow.Arrow>(includeInactive: true);
            foreach (var arrow in stickedArrows)
                arrow.Despawn(); 
        }
        
        private void Die()
        {
            Despawn();
        }

        private void SetStats()
        {
            var stats = _enemySettingsDatabase.GetByType(_type);
            _health.Value = stats.maxHealth;
            _movementType = stats.movementType;
        }

        private void Despawn()
        {
            _enemyPool.Despawn(this); 
            _behaviour.Value.Cleanup();
        }
        
        public class EnemyPool : MonoMemoryPool<Vector3, IEnemyBehaviour, EEnemyType, EnemyBase>
        {
            protected override void Reinitialize(Vector3 pos, IEnemyBehaviour behaviour, EEnemyType type, EnemyBase enemy)
            {
                enemy.OnSpawned(pos, behaviour, type);
            }

            protected override void OnDespawned(EnemyBase enemy)
            {
                enemy.OnDespawned();
            }
        }
        
    }
}