using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

internal static class MuseumDatabase {
    private const string FileName = "museum.dat";

    private static List<Exhibit> _exhibits = new();

    public static void Load() {
        if (!File.Exists(FileName)) {
            return;
        }

        try {
            using var fileStream = new FileStream(FileName, FileMode.Open);

#pragma warning disable SYSLIB0011
            var binaryFormatter = new BinaryFormatter();

            _exhibits =
                (List<Exhibit>)binaryFormatter.Deserialize(fileStream);
#pragma warning restore SYSLIB0011
        }
        catch {
            Console.WriteLine("Ошибка чтения файла.");
        }
    }

    public static void Save() {
        try {
            using var fileStream =
                new FileStream(FileName, FileMode.Create);

#pragma warning disable SYSLIB0011
            var binaryFormatter = new BinaryFormatter();

            binaryFormatter.Serialize(fileStream, _exhibits);
#pragma warning restore SYSLIB0011
        }
        catch {
            Console.WriteLine("Ошибка сохранения.");
        }
    }

    public static void Add(Exhibit exhibit) {
        _exhibits.Add(exhibit);
    }

    public static void ShowAll() {
        if (!_exhibits.Any()) {
            Console.WriteLine("База данных пуста.");
            return;
        }

        foreach (var exhibit in _exhibits) {
            Console.WriteLine(exhibit);
        }
    }

    public static bool DeleteById(int id) {
        var exhibit = _exhibits.FirstOrDefault(x => x.Id == id);

        if (exhibit == null) {
            return false;
        }

        _exhibits.Remove(exhibit);

        return true;
    }

    public static IEnumerable<Exhibit> GetByCentury(int century) {
        return _exhibits.Where(x => x.Century == century);
    }

    public static IEnumerable<Exhibit> GetByFirstLetter(char letter) {
        return _exhibits.Where(x =>
            x.Name.StartsWith(
                letter.ToString(),
                StringComparison.OrdinalIgnoreCase));
    }

    public static bool Exists(string name) {
        return _exhibits.Any(x =>
            x.Name.Equals(
                name,
                StringComparison.OrdinalIgnoreCase));
    }

    public static int CountExhibits() {
        return _exhibits.Count;
    }
}
