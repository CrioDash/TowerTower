namespace Gameplay.Aim
{
    public interface IAimVisualizer
    {
        void UpdateSpline(float pullFactor);
        void ResetSpline();
    }
}