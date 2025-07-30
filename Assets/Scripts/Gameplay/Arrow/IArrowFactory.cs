using UnityEngine;

namespace Gameplay.Arrow
{
    public interface IArrowFactory
    {
        Arrow Create(Vector3 position, Vector3 angle, Vector3 force);
    }
}