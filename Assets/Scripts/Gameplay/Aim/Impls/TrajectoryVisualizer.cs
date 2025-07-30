using Db.PlayerAimDatabase;
using UnityEngine;

namespace Gameplay.Aim.Impls
{
    public class TrajectoryVisualizer : ITrajectoryVisualizer
    {
        private readonly LineRenderer _lineRenderer;
        private readonly Transform _trajectoryTransform;
        private readonly IPlayerAimDatabase _playerAimDatabase;

        public TrajectoryVisualizer(
            Transform trajectoryTransform,
            IPlayerAimDatabase playerAimDatabase)
        {
            _trajectoryTransform = trajectoryTransform;
            _playerAimDatabase = playerAimDatabase;
            _lineRenderer = _trajectoryTransform.GetComponent<LineRenderer>();
        }
        
        public void DisableTrajectory()
        {
            _lineRenderer.positionCount = 0;
        }
        

        public void DrawTrajectory(Vector2 velocity)
        {
            _trajectoryTransform.eulerAngles = Vector3.zero;
            _lineRenderer.positionCount = _playerAimDatabase.TrajectorySegmentsCount;
        
            for (int i = 0; i < _playerAimDatabase.TrajectorySegmentsCount; i++)
            {
                float t = i * _playerAimDatabase.TrajectoryTimeStep;
                
                Vector3 displacement = velocity * t + Physics2D.gravity * (0.5f * t * t);
                Vector3 drawPoint = displacement;
            
                _lineRenderer.SetPosition(i, drawPoint);
            }
        }
    }
}