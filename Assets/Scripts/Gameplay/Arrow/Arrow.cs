using System;
using System.Collections.Generic;
using System.Linq;
using Db.ArrowSettingsDatabase;
using Gameplay.Arrow.Impls;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.Arrow
{
    public class Arrow : MonoBehaviour, IPoolable<Vector3, Vector3, Vector3>
    {
        private ArrowPool _arrowPool;
        private PooledArrowFactory _arrowFactory;
        private Rigidbody2D _rigidbody;
        private Collider2D _collider;
        private IArrowHitHandler[] _hitHandlers;
        private IArrowSettingsDatabase _arrowSettingsDatabase;
        
        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        
        private bool _hasBeenReturned;
        
        [Inject]
        private void Construct(
            ArrowPool arrowPool,
            PooledArrowFactory pooledArrowFactory,
            IArrowHitHandler[] hitHandlers,
            IArrowSettingsDatabase arrowSettingsDatabase)
        {
            _arrowFactory = pooledArrowFactory;
            _arrowPool = arrowPool;
            _hitHandlers = hitHandlers;
            _arrowSettingsDatabase = arrowSettingsDatabase;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
        }

        private void LateUpdate()
        {
            if (_rigidbody.bodyType == RigidbodyType2D.Static || _rigidbody.linearVelocity.magnitude < 0.01f) return;
            
            Vector2 v = _rigidbody.linearVelocity.normalized;
            
            transform.up = v;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_hitHandlers.Any(handler => handler.HandleHit(this, other)))
            {
                Observable.Timer(TimeSpan.FromSeconds(_arrowSettingsDatabase.DespawnDelay))
                    .Subscribe(_ => Despawn())
                    .AddTo(_disposables);
            }
        }

        public void OnSpawned(Vector3 position, Vector3 angle, Vector3 force)
        {
            _disposables.Clear();
            _hasBeenReturned = false; 
            
            transform.position = position;

            angle.z += 90f;
            transform.eulerAngles = angle;
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody.constraints = RigidbodyConstraints2D.None;
            _rigidbody.AddForce(force, ForceMode2D.Impulse);
            
            _collider.enabled = true;
        }

        public void OnDespawned()
        {
            _disposables.Clear();
            gameObject.SetActive(false);
            gameObject.transform.SetParent(_arrowFactory.ArrowContainer, true);
        }

        public void Despawn()
        {
            if (_hasBeenReturned) return;
            _hasBeenReturned = true;
            _arrowPool.Despawn(this);
        }
        
        public void StickInto(Transform parent)
        {
            _rigidbody.linearVelocity = Vector2.zero;
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
            
            
            _collider.enabled = false;
            
            transform.SetParent(parent, true);
        }
        
        public class ArrowPool : MonoMemoryPool<Vector3, Vector3, Vector3, Arrow>
        {
            protected override void Reinitialize(Vector3 pos, Vector3 angle, Vector3 force, Arrow arrow)
            {
                arrow.OnSpawned(pos, angle, force);
            }

            protected override void OnDespawned(Arrow arrow)
            {
                arrow.OnDespawned();
            }
        }
    }
}