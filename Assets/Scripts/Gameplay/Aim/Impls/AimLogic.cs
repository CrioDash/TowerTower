using Db.PlayerAimDatabase;
using Enums;
using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Aim.Impls
{
    public class AimLogic : IAimLogic
    {
        private readonly IPlayerAimDatabase _playerAimDatabase;
        private readonly IPlayerChangeSideService _sideService;

        public AimLogic(
            IPlayerAimDatabase playerAimDatabase,
            IPlayerChangeSideService sideService)
        {
            _playerAimDatabase = playerAimDatabase;
            _sideService = sideService;
        }

        public float CalculatePullFactor(Vector2 dragStart, Vector2 current)
        {
            float magnitude = Vector2.Distance(current, dragStart) / 5f;
            return Mathf.Clamp01(magnitude / _playerAimDatabase.StringMagnitude);
        }

        public float CalculateAngle(Vector2 dragStart, Vector2 current, EPlayerSide side)
        {
            Vector2 direction = side == EPlayerSide.Left ? current - dragStart : dragStart - current;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle = Mathf.Clamp(angle, -80f, 80f);

            return side == EPlayerSide.Right ? -angle : angle;
        }
    }
}