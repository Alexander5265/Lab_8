using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

public static class MuseumDatabase
{
    private static string fileName = "museum.dat";

    public static List<Exhibit> exhibits = new List<Exhibit>();

    // Загрузка
    public static void Load()
    {
        if (!File.Exists(fileName))
            return;

        try
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);

#pragma warning disable SYSLIB0011
            BinaryFormatter bf = new BinaryFormatter();
            exhibits = (List<Exhibit>)bf.Deserialize(fs);
#pragma warning restore SYSLIB0011

            fs.Close();
        }
        catch
        {
            Console.WriteLine("Ошибка чтения файла.");
        }
    }

    // Сохранение
    public static void Save()
    {
        try
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);

#pragma warning disable SYSLIB0011
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, exhibits);
#pragma warning restore SYSLIB0011

            fs.Close();
        }
        catch
        {
            Console.WriteLine("Ошибка сохранения.");
        }
    }

    // Добавление
    public static void Add(Exhibit exhibit)
    {
        exhibits.Add(exhibit);
    }

    // Просмотр
    public static void ShowAll()
    {
        if (!exhibits.Any())
        {
            Console.WriteLine("База данных пуста.");
            return;
        }

        foreach (var exhibit in exhibits)
        {
            Console.WriteLine(exhibit);
        }
    }

    // Удаление по ключу
    public static bool DeleteById(int id)
    {
        var exhibit = exhibits.FirstOrDefault(x => x.Id == id);

        if (exhibit == null)
            return false;

        exhibits.Remove(exhibit);
        return true;
    }

    // ===== ЗАПРОСЫ =====

    // 1. Перечень экспонатов определенного века
    public static IEnumerable<Exhibit> GetByCentury(int century)
    {
        return exhibits.Where(x => x.Century == century);
    }

    // 2. Перечень экспонатов по первой букве
    public static IEnumerable<Exhibit> GetByFirstLetter(char letter)
    {
        return exhibits.Where(x =>
            x.Name.StartsWith(letter.ToString(),
            StringComparison.OrdinalIgnoreCase));
    }

    // 3. Проверка наличия экспоната
    public static bool Exists(string name)
    {
        return exhibits.Any(x =>
            x.Name.Equals(name,
            StringComparison.OrdinalIgnoreCase));
    }

    // 4. Количество экспонатов
    public static int CountExhibits()
    {
        return exhibits.Count();
    }
}