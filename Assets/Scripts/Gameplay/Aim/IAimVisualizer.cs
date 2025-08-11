namespace Gameplay.Aim
{
    public interface IAimVisualizer
    { 
        void UpdateLineRenderer(float currentPull);
        void ResetLineRenderer();
    }
}