using Enums;
using UnityEngine;

namespace Gameplay.Aim
{
    public interface IAimLogic
    {
        float CalculatePullFactor(Vector2 dragStart, Vector2 current);
        float CalculateAngle(Vector2 dragStart, Vector2 current, EPlayerSide side);
    }
}