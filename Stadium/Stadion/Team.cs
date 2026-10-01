using System;

/// <summary>
/// Связана с Coach через внешний ключ CoachId
/// </summary>
public class Team
{
    /// <summary>
    /// Первичный ключ 
    /// </summary>
    public int Id { get; set; }
    public string Name { get; set; }
    /// <summary>
    /// Внешний ключ на Coach.Id
    /// </summary>
    public int CoachId { get; set; }
    public string City { get; set; }
    /// <summary>
    /// Тип decimal — деньги, не double
    /// </summary>
    public decimal Budget { get; set; }
    /// <summary>
    /// Вычисляемое свойство: true, если бюджет больше 100 000 000 руб
    /// </summary>
    public bool IsRich => Budget > 100000000m;
    public Team()
    {
    }

    /// <summary>
    /// Конструктор с валидацией правил предметной области
    /// </summary>
    public Team(int id, string name, int coachId, string city, decimal budget)
    {
        /// Проверка: Id положительный
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "Id команды должен быть больше нуля.");

        /// Проверка: CoachId положительный
        if (coachId <= 0)
            throw new ArgumentOutOfRangeException(nameof(coachId), "CoachId должен быть больше нуля.");

        /// Проверка: Name не пустой
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название команды не может быть пустым.", nameof(name));

        /// Проверка: City не пустой
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("Город команды не может быть пустым.", nameof(city));

        /// Проверка: бюджет не отрицательный
        if (budget < 0m)
            throw new ArgumentOutOfRangeException(nameof(budget), "Бюджет не может быть отрицательным.");

        Id = id;
        Name = name;
        CoachId = coachId;
        City = city;
        Budget = budget;
    }

    /// <summary>
    /// Возвращает строку вида "Спартак (Москва, 500000000 руб.)"
    /// </summary>
    public string GetInfo() => $"{Name} ({City}, {Budget} руб.)";
}