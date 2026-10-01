using System;
using System.Collections.Generic;

/// <summary>
/// Точка входа программы. Содержит аналитические методы и консольное меню выбора источника данных
/// </summary>
class Program
{
    /// <summary>
    /// Находит тренера команды по её названию
    /// Сначала ищет команду, затем по Team.CoachId — тренера
    /// </summary>
    static Coach FindCoach(string teamName, List<Team> teams, List<Coach> coaches)
    {
        /// Проверка на null: если что-то из входных данных null — возвращаем null
        if (string.IsNullOrWhiteSpace(teamName)) return null;
        if (teams == null) return null;
        if (coaches == null) return null;

        /// Шаг 1. Ищем команду с указанным названием
        Team foundTeam = null;
        foreach (Team t in teams)
        {
            if (t == null) continue;
            if (t.Name == teamName)
            {
                foundTeam = t;
                break;
            }
        }
        /// Команда не найдена
        if (foundTeam == null) return null;
        /// Шаг 2. По CoachId ищем тренера
        foreach (Coach c in coaches)
        {
            if (c == null) continue;
            if (c.Id == foundTeam.CoachId)
                return c;
        }
        return null;
    }

    /// <summary>
    /// Находит команду, играющую в указанном матче (по Match.TeamId)
    /// </summary>
    static Team FindTeam(Match match, List<Team> teams)
    {
        /// Проверки на null
        if (match == null) return null;
        if (teams == null) return null;

        foreach (Team t in teams)
        {
            if (t == null) continue;
            if (t.Id == match.TeamId)
                return t;
        }
        return null;
    }

    /// <summary>
    /// Считает суммарное количество голов во всех матчах
    /// Для пустого списка возвращает 0
    /// </summary>
    static int GetTotalGoals(List<Match> matches)
    {
        /// Проверка на null
        if (matches == null) return 0;

        int total = 0;
        foreach (Match m in matches)
        {
            if (m == null) continue;
            total += m.GetGoals();
        }
        return total;
    }

    /// <summary>
    /// Формирует таблицу очков команд по результатам матчей
    /// Победа — 3 очка, ничья — 1, поражение — 0
    /// </summary>
    static Dictionary<string, int> GetTeamStats(List<Match> matches, List<Team> teams)
    {
        /// Словарь результата
        Dictionary<string, int> stats = new Dictionary<string, int>();

        /// Проверки на null
        if (matches == null) return stats;
        if (teams == null) return stats;

        /// Шаг 1. Инициализируем команды нулями
        foreach (Team t in teams)
        {
            if (t == null) continue;
            if (!stats.ContainsKey(t.Name))
                stats[t.Name] = 0;
        }

        /// Шаг 2. Обрабатываем матчи
        foreach (Match m in matches)
        {
            if (m == null) continue;

            Team host = FindTeam(m, teams);
            if (host == null) continue;

            string hostName = host.Name;
            string guestName = m.Opponent;

            /// Защита от null у Opponent
            if (string.IsNullOrWhiteSpace(guestName)) continue;

            if (!stats.ContainsKey(hostName)) stats[hostName] = 0;
            if (!stats.ContainsKey(guestName)) stats[guestName] = 0;

            /// Защита от null у Score
            if (string.IsNullOrWhiteSpace(m.Score)) continue;

            string[] parts = m.Score.Split(':');
            if (parts.Length != 2) continue;

            int hostGoals;
            int guestGoals;
            if (!int.TryParse(parts[0], out hostGoals)) continue;
            if (!int.TryParse(parts[1], out guestGoals)) continue;

            if (hostGoals > guestGoals)
            {
                stats[hostName] += 3;
            }
            else if (hostGoals < guestGoals)
            {
                stats[guestName] += 3;
            }
            else
            {
                stats[hostName] += 1;
                stats[guestName] += 1;
            }
        }

        return stats;
    }

    /// <summary>
    /// Печатает все матчи в формате:
    /// "Спартак — Зенит (2:1, 01.09.2025)" — команда "Спартак", тренер Иванов И.И.
    /// </summary>
    static void PrintAllMatches(List<Match> matches, List<Team> teams, List<Coach> coaches)
    {
        /// Проверка на null
        if (matches == null) return;

        foreach (Match m in matches)
        {
            if (m == null) continue;

            Team team = FindTeam(m, teams);
            string teamName = team != null ? team.Name : "—";
            string teamInfo = team != null ? $"\"{team.Name}\"" : "\"—\"";

            /// Ищем тренера команды
            Coach coach = null;
            if (team != null && coaches != null)
            {
                foreach (Coach c in coaches)
                {
                    if (c == null) continue;
                    if (c.Id == team.CoachId)
                    {
                        coach = c;
                        break;
                    }
                }
            }

            string coachName = coach != null ? coach.FullName : "—";
            Console.WriteLine($"{m.GetInfo(teamName)} — команда {teamInfo}, тренер {coachName}");
        }
    }

    /// <summary>
    /// Точка входа. Запрашивает источник данных и выполняет все методы.
    /// </summary>
    static void Main()
    {
        /// Настраиваем кодировку вывода
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        /// Объявляем коллекции заранее
        List<Coach> coaches = null;
        List<Team> teams = null;
        List<Match> matches = null;

        /// Блок try/catch для всего ввода и загрузки данных
        try
        {
            Console.WriteLine("Выберите источник данных:");
            Console.WriteLine("1 - InMemory");
            Console.WriteLine("2 - CSV (папка data)");
            Console.Write("Ваш выбор: ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    {
                        InMemoryRepository repo = new InMemoryRepository();
                        coaches = repo.GetCoaches();
                        teams = repo.GetTeams();
                        matches = repo.GetMatches();
                        break;
                    }
                case 2:
                    {
                        CsvRepository repo = new CsvRepository("data");
                        coaches = repo.GetCoaches();
                        teams = repo.GetTeams();
                        matches = repo.GetMatches();
                        break;
                    }
                default:
                    Console.WriteLine("Неверный выбор");
                    return;
            }
        }
        catch (Exception ex)
        {
            /// При любой ошибке ввода/загрузки — сообщаем и выходим
            Console.WriteLine($"Ошибка ввода данных: {ex.Message}");
            return;
        }

        /// 1. Поиск тренера команды
        Coach coach = FindCoach("Спартак", teams, coaches);
        Console.WriteLine($"1. FindCoach(\"Спартак\"): {(coach != null ? coach.GetInfo() : "null")}");

        /// 2. Поиск команды матча
        Match sampleMatch = matches.Count > 0 ? matches[0] : null;
        Team teamOfMatch = FindTeam(sampleMatch, teams);

        /// Для вывода GetInfo() подставим найденное имя или "—"
        string sampleTeamName = teamOfMatch != null ? teamOfMatch.Name : "—";
        string sampleMatchInfo = sampleMatch != null
            ? sampleMatch.GetInfo(sampleTeamName)
            : "—";

        Console.WriteLine($"2. FindTeam(match \"{sampleMatchInfo}\"): {(teamOfMatch != null ? teamOfMatch.GetInfo() : "null")}");

        /// 3. Общее количество голов
        Console.WriteLine($"3. GetTotalGoals: {GetTotalGoals(matches)}");

        /// 4. Очки команд
        Dictionary<string, int> stats = GetTeamStats(matches, teams);
        Console.Write("4. GetTeamStats: ");
        bool first = true;
        foreach (KeyValuePair<string, int> pair in stats)
        {
            if (!first) Console.Write(", ");
            Console.Write($"{pair.Key} — {pair.Value}");
            first = false;
        }
        Console.WriteLine();

        /// 5. Вывод всех матчей
        Console.WriteLine("5. PrintAllMatches:");
        PrintAllMatches(matches, teams, coaches);

        /// Проверка «не найдено»
        Console.WriteLine();
        Coach unknown = FindCoach("Неизвестная команда", teams, coaches);
        Console.WriteLine($"Не найдено: FindCoach(\"Неизвестная команда\") → {(unknown == null ? "null" : unknown.GetInfo())}");
    }
}