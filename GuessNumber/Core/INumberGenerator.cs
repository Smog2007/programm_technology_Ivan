namespace GuessNumber.Core;

public interface INumberGenerator
{
    int Generate(int min, int max);
}