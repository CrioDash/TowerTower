using Db.PlayerAimDatabase;
using UnityEngine;

namespace Gameplay.Aim.Impls
{
    public class ArrowManipulator : IArrowManipulator
    {
        private readonly Transform _arrowTransform;
        private readonly IPlayerAimDatabase _playerAimDatabase;

        public Vector3 ArrowPos => _arrowTransform.position;

        private Vector3 _pos;
        
        public ArrowManipulator(
            Transform arrowTransform,
            IPlayerAimDatabase playerAimDatabase)
        {
            _arrowTransform = arrowTransform;
            _playerAimDatabase = playerAimDatabase;
        }

        public void UpdateArrow(float pullFactor)
        {
            _pos = _arrowTransform.localPosition;
            _pos.x = pullFactor * _playerAimDatabase.StringMagnitude;
            _arrowTransform.localPosition = _pos;
        }

        public void ResetArrow()
        {
            _pos = _arrowTransform.localPosition;
            _pos.x = 0;
            _arrowTransform.localPosition = _pos;
        }
    }
}