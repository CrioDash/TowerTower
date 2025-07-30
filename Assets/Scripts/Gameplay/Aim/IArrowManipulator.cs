using UnityEngine;

namespace Gameplay.Aim
{
    public interface IArrowManipulator
    {
        Vector3 ArrowPos { get; }
        void UpdateArrow(float pullFactor);
        void ResetArrow();
    }
}