using System.Collections.Generic;

namespace GuessNumber.Statistics;

public interface IStatisticsStorage
{
    void Add(GameResult result);
    IReadOnlyList<GameResult> GetAll();
}