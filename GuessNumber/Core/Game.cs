using GuessNumber.IO;
using GuessNumber.Notifications;
using GuessNumber.Statistics;

namespace GuessNumber.Core;

public sealed class Game
{
    private readonly INumberGenerator _generator;
    private readonly IInput _input;
    private readonly IOutput _output;
    private readonly IGameNotifier _notifier;
    private readonly IStatisticsStorage _statistics;
    private readonly IDifficulty _difficulty;
    private readonly string _playerName;

    public Game(
        INumberGenerator generator,
        IInput input,
        IOutput output,
        IGameNotifier notifier,
        IStatisticsStorage statistics,
        IDifficulty difficulty,
        string playerName)
    {
        _generator = generator;
        _input = input;
        _output = output;
        _notifier = notifier;
        _statistics = statistics;
        _difficulty = difficulty;
        _playerName = playerName;
    }

    public void Run()
        => throw new NotImplementedException();
}