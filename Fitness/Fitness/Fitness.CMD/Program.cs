using Fitness.BL.Controller;
using Fitness.BL.Model;
using System;
using System.Runtime.CompilerServices;

namespace Fitness.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветствует приложение Fitness");
            // TODO: Проверка на правильность ввода данных

            Console.Write("Введите имя пользователя: ");
            var name = Console.ReadLine();

            var userController = new UserController(name);
            if(userController.IsNewUser)
            {
                Console.Write("Введите пол: ");
                var gender = Console.ReadLine();

                DateTime birthDate = DateTimeParse();

                double weight = ParseDouble("вес");

                double height = ParseDouble("рост");

                userController.SetNewUserData(gender, birthDate, weight, height);
            }
            Console.WriteLine(userController.CurrentUser);
            
        }

        private static DateTime DateTimeParse()
        {
            DateTime birthDate;
            while (true)
            {
                Console.Write("Введите дату рождения: ");
                if (DateTime.TryParse(Console.ReadLine(), out birthDate))
                    return birthDate;
                else
                    Console.WriteLine("Неверный формат даты");
            }
        }

        private static double ParseDouble(string name)
        {
            while (true)
            {
                Console.Write($"Введите {name}: ");
                if (double.TryParse(Console.ReadLine(), out double value))
                    return value;
                else
                    Console.WriteLine($"Неверный формат {name}а");
            }
        }
    }
}
