using System;

namespace Fitness.BL.Model
{
    [Serializable]
    public class Activity
    {
        /// <summary>
        /// Название активности
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Калории в минуту
        /// </summary>
        public double CaloriesPerMinute { get; set; }

        public Activity(string name, double caloriesPerMinute)
        {
            // TODO: Проверка

            Name = name;
            CaloriesPerMinute = caloriesPerMinute;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
