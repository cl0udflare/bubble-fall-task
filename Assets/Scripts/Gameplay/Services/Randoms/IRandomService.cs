namespace Gameplay.Services.Randoms
{
    public interface IRandomService
    {
        float Value { get; }
        float Range(float min, float max);
        int Range(int minInclusive, int maxExclusive);
    }
}