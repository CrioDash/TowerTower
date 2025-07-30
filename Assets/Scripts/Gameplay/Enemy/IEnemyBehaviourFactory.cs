using Enums;

namespace Gameplay.Enemy
{
    public interface IEnemyBehaviourFactory
    {
        IEnemyBehaviour Create(EBehaviourType type);
    }
}