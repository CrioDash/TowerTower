using Db.PlayerAimDatabase;
using UnityEngine;

namespace Gameplay.Aim.Impls
{
    public class TrajectoryVisualizer : ITrajectoryVisualizer
    {
        private readonly LineRenderer _lineRenderer;
        private readonly Transform _trajectoryTransform;
        private readonly IPlayerAimDatabase _playerAimDatabase;
        
        private Vector3 _displacement;
        private float _timeStep;

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
               _timeStep = i * _playerAimDatabase.TrajectoryTimeStep;
                
                _displacement = velocity * _timeStep + Physics2D.gravity * (0.5f * _timeStep * _timeStep);
                
                _lineRenderer.SetPosition(i, _displacement);
            }
        }
    }
}