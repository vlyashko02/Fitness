using Fitness.BL.Model;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Fitness.BL.Controller
{
    /// <summary>
    /// Контроллер пользователя
    /// </summary>
    public class UserController
    {
        public User User { get; }
        /// <summary>
        /// Создание контроллера
        /// </summary>
        /// <param name="user"> Пользователь </param>
        public UserController(string userName, string genderName, DateTime dateBirth, double weight, double height)
        {
            // TODO: Проверка
            var gender = new Gender(genderName);

            User = new User(userName, gender, dateBirth, weight, height);
        }
        /// <summary>
        /// Сохранить данные пользователя
        /// </summary>
        public void Save()
        {
            var formatter = new BinaryFormatter();

            using var fs = new FileStream("users.dat", FileMode.OpenOrCreate);
                formatter.Serialize(fs, User);
        }
        /// <summary>
        /// Получить данные пользователя
        /// </summary>
        /// <returns> Пользователь </returns>
        public UserController()
        {
            var formatter = new BinaryFormatter();

            using var fs = new FileStream("users.dat", FileMode.OpenOrCreate);
            {
                if (formatter.Deserialize(fs) is User user)
                    User = user;

                // TODO: Ошибка при прочтении пользователя

            }
        }

    }
}
