using Fitness.BL.Controller;
using Fitness.BL.Model;
using System;
using System.Globalization;
using System.Resources;

namespace Fitness.CMD
{
    public class Program
    {
        static CultureInfo culture = CultureInfo.CreateSpecificCulture("");
        static readonly ResourceManager resourceManager = new ResourceManager("Fitness.CMD.Languages.Messages", typeof(Program).Assembly);
        static void Main(string[] args)
        {
            Console.WriteLine("Which language you want: || Какой язык ты хочешь выбрать:");
            Console.WriteLine("Y - English || Английский");
            Console.WriteLine("U - Russian || Русский");

            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey();
            }
            while (key.Key != ConsoleKey.Y && key.Key != ConsoleKey.U);

            if(key.Key == ConsoleKey.Y)
            {
                culture = CultureInfo.CreateSpecificCulture("en-us");
            }
            else if(key.Key == ConsoleKey.U)
            {
                culture = CultureInfo.CreateSpecificCulture("ru-ru");
            }
            Console.Clear();
            
            Console.WriteLine(resourceManager.GetString("Hello", culture) + " ");

            Console.Write(resourceManager.GetString("EnterName", culture) + " ");
            var name = Console.ReadLine();

            var userController = new UserController(name);
            var eatingController = new EatingController(userController.CurrentUser);
            if (userController.IsNewUser)
            {
                Console.Write(resourceManager.GetString("EnterSex", culture) + " ");
                var gender = Console.ReadLine();

                DateTime birthDate = DateTimeParse();

                double weight = ParseDouble(resourceManager.GetString("Weight", culture));

                double height = ParseDouble(resourceManager.GetString("Height", culture));

                userController.SetNewUserData(gender, birthDate, weight, height);
            }
            Console.WriteLine(userController.CurrentUser);

            Console.WriteLine(resourceManager.GetString("WhatWannaDo", culture));
            Console.WriteLine(resourceManager.GetString("KeyCodeE", culture));
            key = Console.ReadKey();
            Console.Clear();
            if(key.Key == ConsoleKey.E)
            {
                var foods = EnterEating();
                eatingController.Add(foods.food, foods.weight);

                foreach (var item in eatingController.Eating.Foods)
                {
                    Console.WriteLine($"\t{item.Key} - {item.Value}");
                }
            }
        }

        private static (Food food, double weight) EnterEating()
        {
            Console.Write(resourceManager.GetString("NameProduct", culture) + " ");
            var food = Console.ReadLine();

            var calories = ParseDouble(resourceManager.GetString("Calories", culture));

            var proteins = ParseDouble(resourceManager.GetString("Proteins", culture));

            var fats = ParseDouble(resourceManager.GetString("Fats", culture));

            var carbs = ParseDouble(resourceManager.GetString("Carbs", culture));

            var weight = ParseDouble(resourceManager.GetString("WeightPortion", culture));

            return (new Food(food, calories, proteins, fats, carbs), weight);

        }

        private static DateTime DateTimeParse()
        {
            DateTime birthDate;
            while (true)
            {
                Console.Write(resourceManager.GetString("WriteDateBirth", culture) + " ");
                if (DateTime.TryParse(Console.ReadLine(), out birthDate))
                    return birthDate;
                else
                    Console.WriteLine(resourceManager.GetString("FalseDate", culture));
            }
        }

        private static double ParseDouble(string name)
        {
            while (true)
            {
                Console.Write(resourceManager.GetString("Enter", culture) + " " + name + ": ");
                if (double.TryParse(Console.ReadLine(), out double value))
                    return value;
                else
                    Console.WriteLine(resourceManager.GetString("FalseFormat", culture) + name);
            }
        }
    }
}
