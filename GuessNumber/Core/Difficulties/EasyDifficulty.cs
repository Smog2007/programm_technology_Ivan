namespace GuessNumber.Core.Difficulties;

public sealed class EasyDifficulty : IDifficulty
{
    public string Name => "Лёгкий";
    public int Min => 1;
    public int Max => 10;
    public int MaxAttempts => 10;
}