using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

public static class MuseumDatabase {
    private static string fileName = "museum.dat";
    public static List<Exhibit> exhibits = new List<Exhibit>();
    
    
    public static void Load() {
        if (!File.Exists(fileName))
            return;

        try {
            FileStream fs = new FileStream(fileName, FileMode.Open);
            
            BinaryFormatter bf = new BinaryFormatter();
            exhibits = (List<Exhibit>)bf.Deserialize(fs);

            fs.Close();
        }
        catch {
            Console.WriteLine("Ошибка чтения файла.");
        }
    }


    public static void Save() {
        try {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, exhibits);

            fs.Close();
        }
        catch {
            Console.WriteLine("Ошибка сохранения.");
        }
    }


    public static void Add(Exhibit exhibit) {
        exhibits.Add(exhibit);
    }


    public static void ShowAll() {
        if (!exhibits.Any()) {
            Console.WriteLine("База данных пуста.");
            return;
        }

        foreach (var exhibit in exhibits) {
            Console.WriteLine(exhibit);
        }
    }


    public static bool DeleteById(int id) {
        var exhibit = exhibits.FirstOrDefault(x => x.Id == id);

        if (exhibit == null)
            return false;

        exhibits.Remove(exhibit);
        return true;
    }

    

    public static IEnumerable<Exhibit> GetByCentury(int century) {
        return exhibits.Where(x => x.Century == century);
    }


    public static IEnumerable<Exhibit> GetByFirstLetter(char letter) {
        return exhibits.Where(x =>
            x.Name.StartsWith(letter.ToString(),
            StringComparison.OrdinalIgnoreCase));
    }


    public static bool Exists(string name) {
        return exhibits.Any(x =>
            x.Name.Equals(name,
            StringComparison.OrdinalIgnoreCase));
    }


    public static int CountExhibits() {
        return exhibits.Count();
    }
}
