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
            var exerciseController = new ExecriseController(userController.CurrentUser);

            if (userController.IsNewUser)
            {
                Console.Write(resourceManager.GetString("EnterSex", culture) + " ");
                var gender = Console.ReadLine();

                DateTime birthDate = DateTimeParse("WriteDateBirth");

                double weight = ParseDouble(resourceManager.GetString("Weight", culture));

                double height = ParseDouble(resourceManager.GetString("Height", culture));

                userController.SetNewUserData(gender, birthDate, weight, height);
            }
            Console.WriteLine(userController.CurrentUser);
            while (true)
            {
                Console.Clear();
                Console.WriteLine(resourceManager.GetString("WhatWannaDo", culture));
                Console.WriteLine(resourceManager.GetString("KeyCodeE", culture));
                Console.WriteLine(resourceManager.GetString("KeyCodeR", culture));
                Console.WriteLine(resourceManager.GetString("KeyCodeG",culture));
                Console.WriteLine(resourceManager.GetString("KeyCodeF", culture));
                Console.WriteLine(resourceManager.GetString("KeyCodeEsc", culture));

                key = Console.ReadKey();
                Console.Clear();
                switch (key.Key)
                {
                    case ConsoleKey.E:
                        var foods = EnterEating();
                        eatingController.Add(foods.food, foods.weight);
                        break;
                    case ConsoleKey.R:
                        var exe = EnterExercise();
                        exerciseController.Add(exe.activity, exe.begin, exe.end);
                        break;
                    case ConsoleKey.F:
                        Console.WriteLine(resourceManager.GetString("ListActivities", culture));
                        foreach (var item in exerciseController.Execrises)
                        {
                            var k = item.Finish.Subtract(item.Start);
                            Console.WriteLine($"{item.Activity} # {item.Start.TimeOfDay} - {item.Finish.TimeOfDay}, {(k.TotalSeconds / 60 + k.TotalMinutes + k.TotalHours * 60) * item.Activity.CaloriesPerMinute} {resourceManager.GetString("SumCalories", culture)}");
                        }
                        Console.WriteLine();
                        Console.WriteLine(resourceManager.GetString("ClickAnyButton", culture));
                        Console.ReadLine();
                        break;
                    case ConsoleKey.G:
                        Console.WriteLine(resourceManager.GetString("ListFood", culture));
                        foreach (var item in eatingController.Eating.Foods)
                        {
                            Console.WriteLine($"{item.Key} - {item.Value}");
                        }
                        Console.WriteLine();
                        Console.WriteLine(resourceManager.GetString("ClickAnyButton", culture));
                        Console.ReadLine();
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private static (DateTime begin, DateTime end, Activity activity) EnterExercise()
        {
            Console.Write(resourceManager.GetString("NameExercise", culture) + " ");
            var name = Console.ReadLine();

            var energy = ParseDouble("CountCalories");

            var activity = new Activity(name, energy);

            var begin = DateTimeParse("BeginDateExercise");
            var end = DateTimeParse("EndDateExercise");

            return (begin, end, activity);
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

        private static DateTime DateTimeParse(string value)
        {
            while (true)
            {
                Console.Write(resourceManager.GetString($"{value}", culture) + " ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                    return date;
                else
                    Console.WriteLine(resourceManager.GetString("FalseDate", culture));
            }
        }

        private static double ParseDouble(string name)
        {
            while (true)
            {
                Console.Write(resourceManager.GetString("Enter", culture) + " " + resourceManager.GetString($"{name}", culture) + ": ");
                if (double.TryParse(Console.ReadLine(), out double value))
                    return value;
                else
                    Console.WriteLine(resourceManager.GetString("FalseFormat", culture) + name);
            }
        }
    }
}
