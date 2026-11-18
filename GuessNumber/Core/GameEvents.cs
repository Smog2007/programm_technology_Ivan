namespace GuessNumber.Core;

public sealed class GuessEventArgs : EventArgs
{
    public int Attempt { get; }
    public int Guess { get; }
    public int Target { get; }

    public bool IsCorrect => Guess == Target;
    public string Hint => Guess < Target ? "больше" : (Guess > Target ? "меньше" : "верно");

    public GuessEventArgs(int attempt, int guess, int target)
    {
        Attempt = attempt;
        Guess = guess;
        Target = target;
    }
}

public sealed class GameFinishedEventArgs : EventArgs
{
    public bool Win { get; }
    public int Attempts { get; }
    public TimeSpan Duration { get; }

    public GameFinishedEventArgs(bool win, int attempts, TimeSpan duration)
    {
        Win = win;
        Attempts = attempts;
        Duration = duration;
    }