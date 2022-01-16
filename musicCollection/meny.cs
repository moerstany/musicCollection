using System;
using MySql.Data.MySqlClient;


namespace musicCollection
{
    public static class Show
    {
        public static void Menu()
        {
            ShowInfo("Выберите информацию. которю хотите посмотреть");
            ShowInfo("1. исполнители");
            ShowInfo("2. Информация о всех песнях");
            ShowInfo("3. Информация о всех дисках исполнителя");
            ShowInfo("4. рейтинг популярности исполнителя по количеству дисков в коллекции");
            ShowInfo("5. Информация о самом долгом произведении");
            ShowInfo("0. Выход из программы");
        }

        static void ShowInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[INFO] {message}");
            Console.ResetColor();
        }
    }
}