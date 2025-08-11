using Db.PlayerAimDatabase;
using UnityEngine;
using UnityEngine.Splines;

namespace Gameplay.Aim.Impls
{
    public class SplineAimVisualizer : IAimVisualizer
    {
        private readonly LineRenderer _lineRenderer;
        private readonly Transform _bowTransform;
        private readonly IPlayerAimDatabase _playerAimDatabase;
        
        private Vector3 _lineRendererPos;

        public SplineAimVisualizer(
            LineRenderer lineRenderer,
            Transform bowTransform,
            IPlayerAimDatabase playerAimDatabase)
        {
            _lineRenderer = lineRenderer;
            _bowTransform = bowTransform;
            _playerAimDatabase = playerAimDatabase;
        }
        
        public void UpdateLineRenderer(float currentPull)
        {
            _lineRendererPos.x = currentPull * _playerAimDatabase.StringMagnitude;
            _lineRenderer.SetPosition(1, _lineRendererPos);
        }

        public void ResetLineRenderer()
        {
            _lineRenderer.SetPosition(1, Vector3.zero);
        }
    }
}