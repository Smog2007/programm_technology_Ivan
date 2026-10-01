using System;
using System.Collections.Generic;

/// <summary>
/// Репозиторий с тестовыми данными, хранящимися в памяти
/// </summary>
public class InMemoryRepository
{
    private List<Coach> _coaches;
    private List<Team> _teams;
    private List<Match> _matches;
    /// <summary>
    /// Конструктор заполняет коллекции тестовыми данными
    /// </summary>
    public InMemoryRepository()
    {
        /// Тренеры
        _coaches = new List<Coach>
        {
            new Coach(1, "Иванов Иван Иванович"),
            new Coach(2, "Петров Пётр Петрович"),
            new Coach(3, "Сидоров Сергей Сергеевич"),
            new Coach(4, "Кузнецов Кирилл Кириллович"),
            new Coach(5, "Смирнов Семён Семёнович")
        };

        /// Команды CoachId — существующий Id из _coaches
        _teams = new List<Team>
        {
            new Team(1, "Спартак",   1, "Москва",          500000000m),
            new Team(2, "Зенит",     2, "Санкт-Петербург", 450000000m),
            new Team(3, "ЦСКА",      3, "Москва",          300000000m),
            new Team(4, "Динамо",    4, "Москва",          150000000m),
            new Team(5, "Локомотив", 5, "Москва",          90000000m)
        };

        /// Матчи TeamId — существующий Id из _teams
        _matches = new List<Match>
        {
            new Match(1, 1, "Зенит",   new DateTime(2025, 9, 1),  "2:1", "Открытие Арена"),
            new Match(2, 3, "Динамо",  new DateTime(2025, 9, 8),  "1:1", "ВЭБ Арена"),
            new Match(3, 2, "Спартак", new DateTime(2025, 9, 15), "3:0", "Газпром Арена"),
            new Match(4, 1, "ЦСКА",    new DateTime(2025, 9, 22), "0:2", "Открытие Арена"),
            new Match(5, 5, "Зенит",   new DateTime(2025, 9, 29), "1:1", "РЖД Арена"),
            new Match(6, 4, "Спартак", new DateTime(2025, 10, 6), "2:2", "ВТБ Арена")
        };
    }

    /// <summary>
    /// Возвращает список тренеров
    /// </summary>
    public List<Coach> GetCoaches() => _coaches;

    /// <summary>
    /// Возвращает список команд
    /// </summary>
    public List<Team> GetTeams() => _teams;

    /// <summary>
    /// Возвращает список матчей
    /// </summary>
    public List<Match> GetMatches() => _matches;
}