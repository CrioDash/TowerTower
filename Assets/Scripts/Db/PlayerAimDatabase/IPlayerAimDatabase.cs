namespace Db.PlayerAimDatabase
{
    public interface IPlayerAimDatabase
    {
        public int StringResolution { get; }
        public float StringMagnitude { get; }
        public float BowMagnitude { get; }
        public string AnimatorBowMagnitude { get; }
        public float ArrowMaxForce { get; }
        public float MinimumPullPower { get; }
        public int TrajectorySegmentsCount { get; }
        public float TrajectoryTimeStep { get; }
    }
}