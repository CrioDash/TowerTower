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

        private float _magnitude;
        private Vector2 _direction = Vector2.zero;
        private float _angle;
        
        public AimLogic(
            IPlayerAimDatabase playerAimDatabase,
            IPlayerChangeSideService sideService)
        {
            _playerAimDatabase = playerAimDatabase;
            _sideService = sideService;
        }

        public float CalculatePullFactor(Vector2 dragStart, Vector2 current)
        {
            _magnitude = Vector2.Distance(current, dragStart) / 5f;
            return Mathf.Clamp01(_magnitude / _playerAimDatabase.StringMagnitude);
        }

        public float CalculateAngle(Vector2 dragStart, Vector2 current, EPlayerSide side)
        {
            _direction = side == EPlayerSide.Left ? current - dragStart : dragStart - current;
            _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

            return side == EPlayerSide.Right ? - _angle : _angle;
        }
    }
}