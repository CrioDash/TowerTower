using UnityEngine;

namespace Gameplay.Aim
{
    public interface ITrajectoryVisualizer
    {
        public void DisableTrajectory();
        public void DrawTrajectory(Vector2 velocity);
    }
}