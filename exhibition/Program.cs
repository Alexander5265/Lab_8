using System;

internal class Program {
    private static void Main() {
        Console.ForegroundColor = ConsoleColor.Cyan;

        MuseumDatabase.Load();

        while (true) {
            Console.Clear();

            Console.WriteLine("=== КАТАЛОГ МУЗЕЙНЫХ ЭКСПОНАТОВ ===");
            Console.WriteLine("1. Просмотр базы");
            Console.WriteLine("2. Добавить экспонат");
            Console.WriteLine("3. Удалить экспонат");
            Console.WriteLine("4. Запросы");
            Console.WriteLine("0. Выход");

            Console.Write("\nВыбор: ");

            string choice = Console.ReadLine();

            switch (choice) {
                case "1":
                    MuseumDatabase.ShowAll();
                    Pause();
                    break;

                case "2":
                    AddExhibit();
                    break;

                case "3":
                    DeleteExhibit();
                    break;

                case "4":
                    QueriesMenu();
                    break;

                case "0":
                    MuseumDatabase.Save();
                    return;

                default:
                    Console.WriteLine("Неверный ввод.");
                    Pause();
                    break;
            }
        }
    }

    private static void AddExhibit() {
        try {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Название: ");
            string name = Console.ReadLine();

            Console.Write("Автор: ");
            string author = Console.ReadLine();

            Console.Write("Век: ");
            int century = int.Parse(Console.ReadLine());

            DateTime creationDate;

            while (true) {
                Console.Write("Дата создания (дд.мм.гггг): ");

                if (DateTime.TryParse(Console.ReadLine(), out creationDate))
                {
                    break;
                }

                Console.WriteLine("Неверный формат даты!");
            }

            Console.Write("Стоимость: ");
            double estimatedValue = double.Parse(Console.ReadLine());

            Console.Write("На выставке (true/false): ");
            bool isOnDisplay = bool.Parse(Console.ReadLine());

            var exhibit = new Exhibit(
                id,
                name,
                author,
                century,
                creationDate,
                estimatedValue,
                isOnDisplay);

            MuseumDatabase.Add(exhibit);

            Console.WriteLine("Экспонат добавлен.");
        }
        catch {
            Console.WriteLine("Ошибка ввода данных.");
        }

        Pause();
    }

    private static void DeleteExhibit() {
        try {
            Console.Write("Введите ID: ");
            int id = int.Parse(Console.ReadLine());

            bool result = MuseumDatabase.DeleteById(id);

            Console.WriteLine(result
                ? "Удаление выполнено."
                : "Экспонат не найден.");
        }
        catch {
            Console.WriteLine("Ошибка ввода.");
        }

        Pause();
    }

    private static void QueriesMenu() {
        Console.Clear();

        Console.WriteLine("1. Экспонаты определенного века");
        Console.WriteLine("2. Экспонаты на определенную букву");
        Console.WriteLine("3. Проверить наличие экспоната");
        Console.WriteLine("4. Количество экспонатов");

        Console.Write("\nВыбор: ");

        string choice = Console.ReadLine();

        switch (choice) {
            case "1":
                Console.Write("Введите век: ");
                int century = int.Parse(Console.ReadLine());

                var byCentury = MuseumDatabase.GetByCentury(century);

                foreach (var exhibit in byCentury) {
                    Console.WriteLine(exhibit);
                }

                break;

            case "2":
                Console.Write("Введите букву: ");
                char letter = char.Parse(Console.ReadLine());

                var byLetter = MuseumDatabase.GetByFirstLetter(letter);

                foreach (var exhibit in byLetter) {
                    Console.WriteLine(exhibit);
                }

                break;

            case "3":
                Console.Write("Введите название: ");
                string name = Console.ReadLine();

                bool exists = MuseumDatabase.Exists(name);

                Console.WriteLine(exists
                    ? "Экспонат найден."
                    : "Экспонат отсутствует.");

                break;

            case "4":
                Console.WriteLine(
                    $"Количество экспонатов: {MuseumDatabase.CountExhibits()}");
                break;

            default:
                Console.WriteLine("Неверный выбор.");
                break;
        }

        Pause();
    }

    private static void Pause() {
        Console.WriteLine("\nНажмите Enter...");
        Console.ReadLine();
    }
}
