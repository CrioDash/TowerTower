namespace Gameplay.Enemy
{
    public interface IEnemyBehaviour
    {
        public int side { get; }
        
        void Initialize(EnemyBase enemy);
        
        void Tick(float deltaTime);
        
        void Cleanup();
    }
}