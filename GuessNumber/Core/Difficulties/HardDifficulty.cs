namespace GuessNumber.Core.Difficulties;

public sealed class HardDifficulty : IDifficulty
{
    public string Name => "Сложный";
    public int Min => 1;
    public int Max => 1000;
    public int MaxAttempts => 10;
}