using System;
using Xunit;
using OrderBot;
using Microsoft.Data.Sqlite;

namespace OrderBot.tests
{
    public class OrderTest
    {
        [Fact]
        public void StringAsCalorieCount()
        {
            SqliteConnection connection = Sqlite.GetConnection();
            // Seeder.Seed();
            Order oOrder = new Order(connection);

            // Welcome message
            Assert.Contains("Welcome", oOrder.OnMessage("hello"));

            // Calorie Message
            Assert.Contains("calories", oOrder.OnMessage("hello"));

            // Calorie Message Again
            Assert.Contains("calories", oOrder.OnMessage("hello"));
        }
        [Fact]
        public void StringAsFatCount()
        {
            SqliteConnection connection = Sqlite.GetConnection();
            // Seeder.Seed();
            Order oOrder = new Order(connection);

            // Welcome message
            Assert.Contains("Welcome", oOrder.OnMessage("hello"));

            // Calorie Message
            Assert.Contains("calories", oOrder.OnMessage("15"));

            // Fat Message
            Assert.Contains("fats", oOrder.OnMessage("15"));

            // Fat Message again
            Assert.Contains("fats", oOrder.OnMessage("hello"));
        }
        [Fact]
        public void StringAsProteinCount()
        {
            SqliteConnection connection = Sqlite.GetConnection();
            // Seeder.Seed();
            Order oOrder = new Order(connection);

            // Welcome message
            Assert.Contains("Welcome", oOrder.OnMessage("hello"));

            // Calorie Message
            Assert.Contains("calories", oOrder.OnMessage("15"));

            // Fat Message
            Assert.Contains("fats", oOrder.OnMessage("20"));

            // Protein Message
            Assert.Contains("proteins", oOrder.OnMessage("20"));

            // Protein Message again
            Assert.Contains("proteins", oOrder.OnMessage("hello"));
        }

        [Fact]
        public void MealFoundTests()
        {
            SqliteConnection connection = Sqlite.GetConnection();
            // Seeder.Seed();
            Order oOrder = new Order(connection);

            // Welcome message
            Assert.Contains("Welcome", oOrder.OnMessage("hello"));

            // Calorie Message
            Assert.Contains("calories", oOrder.OnMessage("hello"));

            // Fat Message
            Assert.Contains("fats", oOrder.OnMessage("20"));

            // Protein Message
            Assert.Contains("proteins", oOrder.OnMessage("15"));

            // Meal found Message
            Assert.Contains("meals we found", oOrder.OnMessage("15"));

            // // Confirmation Message
            Assert.Contains("Yes", oOrder.OnMessage("1"));

            // // Address Message
            Assert.Contains("address", oOrder.OnMessage("yes"));

            // // link Message
            Assert.Contains("link", oOrder.OnMessage("Albert Street"));
        }

        [Fact]
        public void MealNotFoundTests()
        {
            SqliteConnection connection = Sqlite.GetConnection();
            Order oOrder = new Order(connection);

            // Welcome message
            Assert.Contains("Welcome", oOrder.OnMessage("hello"));

            // Calorie Message
            Assert.Contains("calories", oOrder.OnMessage("hello"));

            // Fat Message
            Assert.Contains("fats", oOrder.OnMessage("20"));

            // Protein Message
            Assert.Contains("proteins", oOrder.OnMessage("20"));

            // Meal found Message
            Assert.Contains("sorry", oOrder.OnMessage("20"));
        }

        [Fact]
        public void InvalidNumberForMealSelection()
        {
            SqliteConnection connection = Sqlite.GetConnection();
            // Seeder.Seed();
            Order oOrder = new Order(connection);

            // Welcome message
            Assert.Contains("Welcome", oOrder.OnMessage("hello"));

            // Calorie Message
            Assert.Contains("calories", oOrder.OnMessage("hello"));

            // Fat Message
            Assert.Contains("fats", oOrder.OnMessage("20"));

            // Protein Message
            Assert.Contains("proteins", oOrder.OnMessage("15"));

            // Meal found Message
            Assert.Contains("meals we found", oOrder.OnMessage("15"));

            // // Confirmation Message
            Assert.Contains("Please type a number between 1 and 3", oOrder.OnMessage("5"));
        }
    }
}
