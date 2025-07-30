namespace Gameplay.Enemy.Impls.Behaviours
{
    public class IdleBehavior: IEnemyBehaviour
    {
        private int _side;

        public int side => _side;
        
        public void Initialize(EnemyBase enemy)
        {
            _side = enemy.transform.position.x > 0 ? -1 : 1;
        }

        public void Tick(float dt)
        {
            
        }

        public void Cleanup()
        {
            
        }
    }
}