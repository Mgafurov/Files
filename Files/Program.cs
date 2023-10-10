using System;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Выберите действие: ");
            Console.WriteLine("1: Вывести данные сотрудников на экран ");
            Console.WriteLine("2: Добавить новую запись о сотруднике ");
            Console.WriteLine("3: Выход ");

            int choise;
            if (int.TryParse(Console.ReadLine(), out choise))
            {
                switch (choise)
                {
                    case 1:
                        ShowEmployeesData();
                        break;
                    case 2:
                        AddEmployee();
                        break;
                    case 3:
                        return;
                    default:
                        Console.Write("Некорректный выбор. Попробуйте снова! ");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Попробуйте снова! ");
            }
        }
    }
    static void ShowEmployeesData()
    {
        string fileName = "employes.txt";
        if (File.Exists(fileName))
        {
            string[] lines = File.ReadAllLines(fileName);
            Console.WriteLine("Список сотрудников: ");
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }
        else
        {
            Console.WriteLine("Файл с данными о сотрудниках не найден! ");
        }
    }
    static void AddEmployee()
    {
        Console.Write("Введите Ф.И.О сотрудника: ");
        string fullName = Console.ReadLine();
        Console.Write("Введите возраст сотрудника: ");
        int age;
        if (int.TryParse(Console.ReadLine(), out age))
        {
            Console.WriteLine("Некорректный возраст сотрудника! ");
            return;
        }
        Console.Write("Введите рост сотрудника: ");
        int height;
        if (int.TryParse(Console.ReadLine(), out height))
        {
            Console.WriteLine("Некорректный ввод роста! ");
            return;
        }
        Console.WriteLine("Введите дату рождения сотрудника (дд.мм.гггг): ");
        string birthDay = Console.ReadLine();
        Console.WriteLine("Введите месторождение сотрудника: ");
        string birthPlace = Console.ReadLine();
        string employeeRecord = $"{GetNextEmployeeID()}|{DateTime.Now:dd.mm.yyyy HH:mm}|{fullName}|{age}|{height}|{birthDay}|{birthPlace}";
        string fileName = "employes.txt";
        using(StreamWriter writer = File.AppendText(fileName))
        {
            writer.WriteLine(employeeRecord);
        }
        Console.WriteLine("Запись успешно добавлена! ");
    }
    static int GetNextEmployeeID()
    {
        string fileName = "employes.txt";
        if(File.Exists(fileName))
        {
            string[] lines = File.ReadAllLines(fileName);
            if (lines.Length > 0)
            {
                string lastLine = lines[lines.Length - 1];
                string[] parts = lastLine.Split('|');
                if (parts.Length >= 1 && int.TryParse(parts[0], out int lastID))
                {
                    return lastID + 1;
                }
            }
        }
        return 1;// Если файл не существует или не содержит записей, начнем с 1
    }
}