using Fitness.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fitness.BL.Controller
{
    public class ExecriseController : ControllerBase
    {
        private const string FileNameExercises = "exercises.dat";
        private const string FileNameActivities = "activities.dat";
        private readonly User user;
        public List<Execrise> Execrises { get; set; }
        public List<Activity> Activities { get; set; }
        public ExecriseController(User user)
        {
            this.user = user ?? throw new ArgumentNullException("Пользователь не может быть пустым", nameof(user));
            Execrises = GetAllExercises();
            Activities = GetAllActivities();
        }

        public void Add(Activity activity, DateTime begin, DateTime end)
        {
            var act = Activities.SingleOrDefault(a => a.Name == activity.Name);
            if (act == null)
            {
                Activities.Add(activity);

                var exercise = new Execrise(begin, end, activity, user);
                Execrises.Add(exercise);
            }
            else
            {
                var exercise = new Execrise(begin, end, activity, user);
                Execrises.Add(exercise);
            }
            Save();
        }

        private List<Activity> GetAllActivities()
        {
            return Load<List<Activity>>(FileNameActivities) ?? new List<Activity>();
        }
        private List<Execrise> GetAllExercises()
        {
            return Load<List<Execrise>>(FileNameExercises) ?? new List<Execrise>();
        }
        public void Save()
        {
            Save(FileNameExercises, Execrises);
        }
    }
}
