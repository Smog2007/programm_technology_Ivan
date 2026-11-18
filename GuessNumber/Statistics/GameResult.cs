using System;

namespace GuessNumber.Statistics;

public sealed class GameResult
{
    public string PlayerName { get; set; } = "";
    public string Difficulty { get; set; } = "";
    public bool Win { get; set; }
    public int Attempts { get; set; }
    public int Target { get; set; }
    public DateTime PlayedAt { get; set; }
}