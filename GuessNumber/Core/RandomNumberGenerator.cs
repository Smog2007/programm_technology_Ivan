namespace GuessNumber.Core;

public sealed class RandomNumberGenerator : INumberGenerator
{
    private readonly Random _random = new();

    public int Generate(int min, int max) => _random.Next(min, max + 1);
}