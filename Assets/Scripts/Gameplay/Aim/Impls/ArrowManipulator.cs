using Db.PlayerAimDatabase;
using UnityEngine;

namespace Gameplay.Aim.Impls
{
    public class ArrowManipulator : IArrowManipulator
    {
        private readonly Transform _arrowTransform;
        private readonly IPlayerAimDatabase _playerAimDatabase;

        public Vector3 ArrowPos => _arrowTransform.position;
        
        public ArrowManipulator(
            Transform arrowTransform,
            IPlayerAimDatabase playerAimDatabase)
        {
            _arrowTransform = arrowTransform;
            _playerAimDatabase = playerAimDatabase;
        }

        public void UpdateArrow(float pullFactor)
        {
            Vector3 pos = _arrowTransform.localPosition;
            pos.x = pullFactor * _playerAimDatabase.StringMagnitude;
            _arrowTransform.localPosition = pos;
        }

        public void ResetArrow()
        {
            Vector3 pos = _arrowTransform.localPosition;
            pos.x = 0;
            _arrowTransform.localPosition = pos;
        }
    }
}