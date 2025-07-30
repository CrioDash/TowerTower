using UnityEngine;

namespace Db.PlayerAimDatabase.Impls
{
    [CreateAssetMenu(menuName = "Databases/PlayerAimDatabase", fileName = "PlayerAimDatabase")]
    public class PlayerAimDatabase : ScriptableObject, IPlayerAimDatabase
    {
        [SerializeField] private int stringResolution;
        [SerializeField] private float stringMagnitude;
        [SerializeField] private float bowMagnitude;
        [SerializeField] private string animatorBowMagnitude;
        [SerializeField] private float arrowMaxForce;
        [SerializeField] private float minimumPullPower;
        [SerializeField] private int trajectorySegmentsCount;
        [SerializeField] private float trajectoryTimeStep;

        public int StringResolution => stringResolution;
        public float StringMagnitude => stringMagnitude;
        public float BowMagnitude => bowMagnitude;
        public string AnimatorBowMagnitude => animatorBowMagnitude;
        public float ArrowMaxForce => arrowMaxForce;
        public float MinimumPullPower => minimumPullPower;
        public int TrajectorySegmentsCount => trajectorySegmentsCount;
        public float TrajectoryTimeStep => trajectoryTimeStep;
    }
}