using UnityEngine;

namespace Gameplay.Arrow
{
    public interface IArrowHitHandler
    {
        bool HandleHit(Arrow arrow, Collision2D collision);
    }
}