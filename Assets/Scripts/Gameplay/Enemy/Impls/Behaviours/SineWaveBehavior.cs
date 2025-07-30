using UnityEngine;

namespace Gameplay.Enemy.Impls.Behaviours
{
    public class SineWaveBehavior: IEnemyBehaviour
    {
        readonly float _amplitude;
        readonly float _frequency;
        private Rigidbody2D _rb;
        private Vector2 _startPos;
        private float _speed;
        private int _side;

        public int side => _side;

        public SineWaveBehavior(float amplitude, float frequency, float speed)
        {
            _amplitude = amplitude;
            _frequency = frequency;
            _speed = speed;
        }

        public void Initialize(EnemyBase enemy)
        {
            _rb = enemy.GetComponent<Rigidbody2D>();
            _startPos = _rb.position;
            _side = enemy.transform.position.x > 0 ? -1 : 1;
        }
        
        public void Tick(float dt)
        {
            var pos = _rb.position;
            float t = Time.time * _frequency;
            pos.y = _startPos.y + Mathf.Sin(t) * _amplitude;
            pos.x += _speed * _side * dt;
            _rb.MovePosition(pos);
        }

        public void Cleanup() {}
    }
}