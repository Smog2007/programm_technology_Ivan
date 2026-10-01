using System;

/// <summary>
/// Связан с Team через внешний ключ TeamId
/// </summary>
public class Match
{
    /// <summary>
    /// Первичный ключ
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Внешний ключ на Team.Id — команда-хозяин поля
    /// </summary>
    public int TeamId { get; set; }
    public string Opponent { get; set; }
    /// <summary>
    /// Дата матча хранится в формате dd.MM.yyyy
    /// </summary>
    public DateTime Date { get; set; }
    /// <summary>
    /// Счёт матча в формате "X:Y", например "2:1"
    /// </summary>
    public string Score { get; set; }
    public string Stadium { get; set; }
    public Match()
    {
    }
    /// <summary>
    /// Конструктор с валидацией правил предметной области
    /// </summary>
    public Match(int id, int teamId, string opponent, DateTime date, string score, string stadium)
    {
        /// Проверка: Id положительный
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "Id матча должен быть больше нуля.");

        /// Проверка: TeamId положительный
        if (teamId <= 0)
            throw new ArgumentOutOfRangeException(nameof(teamId), "TeamId должен быть больше нуля.");

        /// Проверка: Opponent не пустой
        if (string.IsNullOrWhiteSpace(opponent))
            throw new ArgumentException("Соперник не может быть пустым.", nameof(opponent));

        /// Проверка: Score не пустой
        if (string.IsNullOrWhiteSpace(score))
            throw new ArgumentException("Счёт не может быть пустым.", nameof(score));

        /// Проверка: Score соответствует формату "X:Y"
        string[] scoreParts = score.Split(':');
        if (scoreParts.Length != 2)
            throw new ArgumentException("Счёт должен быть в формате \"X:Y\".", nameof(score));

        int homeGoals;
        int awayGoals;
        if (!int.TryParse(scoreParts[0], out homeGoals) || !int.TryParse(scoreParts[1], out awayGoals))
            throw new ArgumentException("Счёт должен содержать целые числа в формате \"X:Y\".", nameof(score));

        /// Проверка: Stadium не пустой
        if (string.IsNullOrWhiteSpace(stadium))
            throw new ArgumentException("Стадион не может быть пустым.", nameof(stadium));

        Id = id;
        TeamId = teamId;
        Opponent = opponent;
        Date = date;
        Score = score;
        Stadium = stadium;
    }

    /// <summary>
    /// Считает суммарное количество голов в матче из строки Score
    /// Если строка имеет неверный формат — возвращает 0
    /// </summary>
    public int GetGoals()
    {
        /// Защита от null
        if (string.IsNullOrWhiteSpace(Score)) return 0;

        /// Разбиваем "2:1" на ["2", "1"]
        string[] parts = Score.Split(':');

        /// Если частей не ровно 2 — формат некорректен
        if (parts.Length != 2) return 0;

        /// Голы хозяев и гостей
        int homeGoals;
        int awayGoals;

        /// TryParse безопаснее Parse
        if (!int.TryParse(parts[0], out homeGoals)) return 0;
        if (!int.TryParse(parts[1], out awayGoals)) return 0;

        /// Возвращаем сумму голов
        return homeGoals + awayGoals;
    }

    /// <summary>
    /// Возвращает строку вида "Спартак — Зенит (2:1, 01.09.2025)"
    /// </summary>
    public string GetInfo(string teamName) => $"{teamName} — {Opponent} ({Score}, {Date:dd.MM.yyyy})";
}