using System;

namespace Fitness.BL.Model
{
    [Serializable]
    public class Execrise
    {
        /// <summary>
        /// Время начало упражнения
        /// </summary>
        public DateTime Start { get; set; }
        /// <summary>
        /// Время окончания упражнения
        /// </summary>
        public DateTime Finish { get; set; }
        /// <summary>
        /// Упражнение
        /// </summary>
        public Activity Activity { get; set; }
        /// <summary>
        /// Пользователь
        /// </summary>
        public User User { get; set; }

        public Execrise(DateTime start, DateTime finish, Activity activity, User user)
        {
            // TODO: Проверка

            Start = start;
            Finish = finish;
            Activity = activity;
            User = user;
        }

    }
}
