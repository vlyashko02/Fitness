using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fitness.BL.Controller;
using System;
using System.Collections.Generic;
using System.Text;
using Fitness.BL.Model;
using System.Linq;

namespace Fitness.BL.Controller.Tests
{
    [TestClass()]
    public class ExecriseControllerTests
    {
        [TestMethod()]
        public void AddTest()
        {
            // Arrange
            var activityName = Guid.NewGuid().ToString();
            var random = new Random();
            var userName = Guid.NewGuid().ToString();
            var userController = new UserController(userName);
            var exerciseController = new ExecriseController(userController.CurrentUser);
            var activity = new Activity(activityName, random.Next(10, 20));

            // Act

            exerciseController.Add(activity, DateTime.Now, DateTime.Now.AddHours(1));

            // Assert

            Assert.AreEqual(activityName, exerciseController.Activities.First().Name);
        }
    }
}