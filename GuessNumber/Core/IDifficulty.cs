namespace GuessNumber.Core;

public interface IDifficulty
{
    string Name { get; }
    int Min { get; }
    int Max { get; }
    int MaxAttempts { get; }
}