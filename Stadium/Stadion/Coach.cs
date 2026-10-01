using System;

public class Coach
{
    public int Id { get; set; }

    /// <summary>
    /// Полное имя тренера, например "Иванов Иван Иванович"
    /// </summary>
    public string FullName { get; set; }
    public Coach()
    {
    }

    /// <summary>
    /// Конструктор с валидацией правил предметной области
    /// </summary>
    public Coach(int id, string fullName)
    {
        /// Проверка правила: Id должен быть положительным
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "Id тренера должен быть больше нуля.");

        /// Проверка правила: FullName не может быть пустым
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Полное имя тренера не может быть пустым.", nameof(fullName));

        Id = id;
        FullName = fullName;
    }

    /// <summary>
    /// Возвращает сокращённое ФИО в формате "Иванов И.И."
    /// Если строку не удаётся разбить на 3 части — возвращает исходное значение
    /// </summary>
    public string GetInfo()
    {
        /// Защита от null: если FullName не задан, возвращаем пустую строку
        if (string.IsNullOrWhiteSpace(FullName)) return string.Empty;

        /// Разбиваем строку по пробелу: [Фамилия, Имя, Отчество]
        string[] parts = FullName.Split(' ');

        /// Если частей меньше трёх — возвращаем как есть
        if (parts.Length < 3) return FullName;

        /// Извлекаем фамилию и первые буквы имени/отчества
        string surname = parts[0];
        string firstInitial = parts[1].Substring(0, 1);
        string middleInitial = parts[2].Substring(0, 1);

        /// Склеиваем в формат "Иванов И.И."
        return $"{surname} {firstInitial}.{middleInitial}.";
    }
}