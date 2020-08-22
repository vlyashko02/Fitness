

using Fitness.BL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;

namespace Fitness.BL.Controller
{
    public class EatingController : ControllerBase
    {
        private const string FileNameFoods = "foods.dat";
        private const string FileNameEatings = "eating.dat";
        private readonly User user;
        public List<Food> Foods { get; set; }
        public Eating Eating { get; set; }

        public EatingController(User user)
        {
            this.user = user ?? throw new ArgumentNullException("Пользователь не может быть пустым", nameof(user));

            Foods = GetAllFood();
            Eating = GetEating();
        }
        public void Add(Food food, double weight)
        {
            var product = Foods.SingleOrDefault(f => f.Name == food.Name);
            if (product == null)
            {
                Foods.Add(food);
                Eating.Add(food, weight);
                Save();
            }
            else
            {
                Eating.Add(product, weight);
                Save();
            }
        }
        private List<Food> GetAllFood()
        {
            return Load<List<Food>>(FileNameFoods) ?? new List<Food>();
        }
        private Eating GetEating()
        {
            return Load<Eating>(FileNameEatings) ?? new Eating(user);
        }
        public void Save()
        {
            Save(FileNameFoods, Foods);
            Save(FileNameEatings, Eating);
        }
    }
}
