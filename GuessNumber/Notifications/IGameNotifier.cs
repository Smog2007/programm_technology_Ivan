using System;
using GuessNumber.Core;

namespace GuessNumber.Notifications;

public interface IGameNotifier
{
    event EventHandler<GuessEventArgs>? GuessMade;
    event EventHandler<GameFinishedEventArgs>? GameFinished;

    void RaiseGuess(GuessEventArgs args);
    void RaiseGameFinished(GameFinishedEventArgs args);
}