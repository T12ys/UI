class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 4)
        {
            Console.WriteLine("Ошибка: недостаточно аргументов. Нужно два числа, путь к лог-файлу и флаг DEBUG.");
            WaitForExit();
            return;
        }

        string logFilePath = args[2];
        bool debug = args[3].ToLower() == "true";
        
        if (!double.TryParse(args[0], out double a))
        {
            LogError($"Некорректный аргумент: {args[0]}", logFilePath, debug);
            WaitForExit();
            return;
        }
        
        if (!double.TryParse(args[1], out double b))
        {
            LogError($"Некорректный аргумент: {args[1]}", logFilePath, debug);
            WaitForExit();
            return;
        }
        
        Console.WriteLine($"Первое число: {a}");
        Console.WriteLine($"Второе число: {b}");
        Console.WriteLine($"Сумма: {a + b}");

        WaitForExit();
    }

    static void LogError(string message, string logFilePath, bool debug)
    {
        Console.WriteLine($"Ошибка: {message}");

        if (debug && !string.IsNullOrEmpty(logFilePath))
        {
            try
            {
                File.AppendAllText(logFilePath, message + Environment.NewLine);
            }
            catch
            {
                Console.WriteLine("Не удалось записать лог в файл!");
            }
        }
    }
    static void WaitForExit()
    {
        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}