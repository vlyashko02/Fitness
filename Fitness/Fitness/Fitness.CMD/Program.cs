using Fitness.BL.Controller;
using Fitness.BL.Model;
using System;

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

            Console.Write("Введите пол: ");
            var gender = Console.ReadLine();

            //TODO: TryParse вместо Parse

            Console.Write("Введите дату рождения: ");
            var birthDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Введите вес: ");
            double weight = double.Parse(Console.ReadLine());

            Console.Write("Введите рост: ");
            double height = double.Parse(Console.ReadLine());

            var userController = new UserController(name, gender, birthDate, weight, height);
            userController.Save();
            
        }
    }
}
