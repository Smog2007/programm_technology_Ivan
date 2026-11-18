namespace GuessNumber.Core.Difficulties;

public sealed class MediumDifficulty : IDifficulty
{
    public string Name => "Средний";
    public int Min => 1;
    public int Max => 100;
    public int MaxAttempts => 10;
}