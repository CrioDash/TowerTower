using Db.PlayerAimDatabase;
using UnityEngine;
using UnityEngine.Splines;

namespace Gameplay.Aim.Impls
{
    public class SplineAimVisualizer : IAimVisualizer
    {
        private readonly SplineContainer _spline;
        private readonly LineRenderer _lineRenderer;
        private readonly Transform _bowTransform;
        private readonly IPlayerAimDatabase _playerAimDatabase;
        private readonly Vector2 _initialYBounds;

        public SplineAimVisualizer(
            SplineContainer splineContainer,
            LineRenderer lineRenderer,
            Transform bowTransform,
            IPlayerAimDatabase playerAimDatabase)
        {
            _spline = splineContainer;
            _lineRenderer = lineRenderer;
            _bowTransform = bowTransform;
            _playerAimDatabase = playerAimDatabase;

            _initialYBounds = new Vector2(
                splineContainer.Spline[0].Position.y,
                splineContainer.Spline[2].Position.y
            );
        }

        public void UpdateSpline(float pullFactor)
        {
            var splineData = _spline.Spline;
            
            // Middle knot (x - натяжение)
            var middle = splineData[1];
            Vector3 pos = middle.Position;
            pos.x = pullFactor * _playerAimDatabase.StringMagnitude;
            middle.Position = pos;
            splineData.SetKnot(1, middle);

            // Верхний и нижний край
            var left = splineData[0];
            var right = splineData[2];

            left.Position = new Vector3(left.Position.x, _initialYBounds.x + pullFactor * _playerAimDatabase.BowMagnitude, left.Position.z);
            right.Position = new Vector3(right.Position.x, _initialYBounds.y - pullFactor * _playerAimDatabase.BowMagnitude, right.Position.z);

            splineData.SetKnot(0, left);
            splineData.SetKnot(2, right);

            UpdateLineRenderer();
        }

        public void ResetSpline()
        {
            var splineData = _spline.Spline;

            // Сброс позиций
            var middle = splineData[1];
            middle.Position = new Vector3(0, middle.Position.y, middle.Position.z);
            splineData.SetKnot(1, middle);

            var left = splineData[0];
            var right = splineData[2];

            left.Position = new Vector3(left.Position.x, _initialYBounds.x, left.Position.z);
            right.Position = new Vector3(right.Position.x, _initialYBounds.y, right.Position.z);

            splineData.SetKnot(0, left);
            splineData.SetKnot(2, right);

            UpdateLineRenderer();
        }

        private void UpdateLineRenderer()
        {
            _lineRenderer.positionCount = _playerAimDatabase.StringResolution + 1;

            for (int i = 0; i <= _playerAimDatabase.StringResolution; i++)
            {
                float t = i / (float)_playerAimDatabase.StringResolution;
                Vector3 pos = _spline.EvaluatePosition(t);
                Vector3 localPos = _bowTransform.InverseTransformPoint(pos);
                _lineRenderer.SetPosition(i, localPos);
            }
        }
    }
}