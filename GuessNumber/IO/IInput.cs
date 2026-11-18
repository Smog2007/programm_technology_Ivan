namespace GuessNumber.IO;

public interface IInput
{
    int ReadInt(string prompt, int min, int max);
}