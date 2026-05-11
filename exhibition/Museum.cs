using System;

public class Exhibit {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public int Century { get; set; }
    public DateTime CreationDate { get; set; }
    public double EstimatedValue { get; set; }
    public bool IsOnDisplay { get; set; }
    

    public Exhibit(int id, string name, string author,
        int century, DateTime creationDate,
        double estimatedValue, bool isOnDisplay) {
        Id = id;
        Name = name;
        Author = author;
        Century = century;
        CreationDate = creationDate;
        EstimatedValue = estimatedValue;
        IsOnDisplay = isOnDisplay;
    }

    public override string ToString() {
        return $"ID: {Id}\n" +
               $"Название: {Name}\n" +
               $"Автор: {Author}\n" +
               $"Век: {Century}\n" +
               $"Дата создания: {CreationDate.ToShortDateString()}\n" +
               $"Стоимость: {EstimatedValue}\n" +
               $"На выставке: {(IsOnDisplay ? "Да" : "Нет")}\n";
    }
}
