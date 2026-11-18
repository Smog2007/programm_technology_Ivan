using System;

namespace GuessNumber.IO;

public sealed class ConsoleInput : IInput
{
    public int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (int.TryParse(input, out var value) && value >= min && value <= max)
                return value;

            Console.WriteLine($"Введите целое число от {min} до {max}.");
        }
    }
}