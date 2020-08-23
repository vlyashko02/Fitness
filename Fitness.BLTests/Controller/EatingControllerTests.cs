using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Fitness.BL.Model;
using System.Linq;

namespace Fitness.BL.Controller.Tests
{
    [TestClass()]
    public class EatingControllerTests
    {
        [TestMethod()]
        public void AddTest()
        {
            // Arrange
            var foodName = Guid.NewGuid().ToString();
            var random = new Random();
            var userName = Guid.NewGuid().ToString();
            var userController = new UserController(userName);
            var eatingController = new EatingController(userController.CurrentUser);
            var food = new Food(foodName, random.Next(50, 500), random.Next(50, 500), random.Next(50, 500), random.Next(50, 500));

            // Act

            eatingController.Add(food, random.Next(50,200));

            // Assert

            Assert.AreEqual(food.Name, eatingController.Eating.Foods.First().Key.Name);
        }
    }
}