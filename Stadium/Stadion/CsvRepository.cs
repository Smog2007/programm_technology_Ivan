using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

/// <summary>
/// Репозиторий, читающий данные из CSV-файлов в указанной папке
/// Формат файлов: первая строка — заголовки, разделитель — запятая
/// </summary>
public class CsvRepository
{
    /// <summary>
    /// Приватное поле — путь к папке с CSV-файлами
    /// </summary>
    private string _basePath;

    /// <summary>
    /// Создаёт репозиторий, привязанный к папке с CSV-файлами
    /// </summary>
    public CsvRepository(string basePath)
    {
        // Проверка правила: путь не должен быть пустым.
        if (string.IsNullOrWhiteSpace(basePath))
            throw new ArgumentException("Путь к папке с CSV не может быть пустым.", nameof(basePath));

        _basePath = basePath;
    }

    /// <summary>
    /// Загружает тренеров из файла coaches.csv
    /// При отсутствии или пустоте файла возвращает пустой список
    /// </summary>
    public List<Coach> GetCoaches()
    {
        /// Результирующий список
        List<Coach> result = new List<Coach>();

        /// Полный путь к файлу
        string path = Path.Combine(_basePath, "coaches.csv");

        /// Если файла нет — пустой список
        if (!File.Exists(path)) return result;

        /// Читаем все строки
        string[] lines = File.ReadAllLines(path);
        /// Если только заголовок — данных нет
        if (lines.Length < 2) return result;
        /// Читаем со строки 1 (пропускаем заголовок)
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] parts = lines[i].Split(',');
            if (parts.Length < 2) continue;

            /// Создаём через конструктор с валидацией
            Coach c = new Coach(
                int.Parse(parts[0]),
                parts[1]);

            result.Add(c);
        }
        return result;
    }

    /// <summary>
    /// Загружает команды из teams.csv
    /// </summary>
    public List<Team> GetTeams()
    {
        List<Team> result = new List<Team>();
        string path = Path.Combine(_basePath, "teams.csv");

        if (!File.Exists(path)) return result;

        string[] lines = File.ReadAllLines(path);
        if (lines.Length < 2) return result;

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] parts = lines[i].Split(',');

            /// Ожидаем 5 колонок: Id, Name, CoachId, City, Budget
            if (parts.Length < 5) continue;

            /// decimal.Parse с InvariantCulture — единый формат
            Team t = new Team(
                int.Parse(parts[0]),
                parts[1],
                int.Parse(parts[2]),
                parts[3],
                decimal.Parse(parts[4], CultureInfo.InvariantCulture));

            result.Add(t);
        }
        return result;
    }
    /// <summary>
    /// Загружает матчи из matches.csv
    /// </summary>
    public List<Match> GetMatches()
    {
        List<Match> result = new List<Match>();
        string path = Path.Combine(_basePath, "matches.csv");

        if (!File.Exists(path)) return result;

        string[] lines = File.ReadAllLines(path);
        if (lines.Length < 2) return result;

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] parts = lines[i].Split(',');

            /// Ожидаем 6 колонок: Id, TeamId, Opponent, Date, Score, Stadium
            if (parts.Length < 6) continue;
            Match m = new Match(
                int.Parse(parts[0]),
                int.Parse(parts[1]),
                parts[2],
                DateTime.ParseExact(parts[3], "dd.MM.yyyy", CultureInfo.InvariantCulture),
                parts[4],
                parts[5]);

            result.Add(m);
        }
        return result;
    }
}