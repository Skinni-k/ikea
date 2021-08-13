using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Sqlite;

namespace OrderBotPage
{
    public static class Seeder
    {
        private static SqliteConnection connection;

        public static void Seed()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "../SqliteDB.db";
            connection = new SqliteConnection(connectionStringBuilder.ConnectionString);
            SeedTables();

        }

        private static void SeedTables()
        {

            using (connection)
            {
                connection.Open();

                //Create a table (drop if already exists first):
                var delMealsTableCmd = connection.CreateCommand();
                delMealsTableCmd.CommandText = "DROP TABLE IF EXISTS meals";
                delMealsTableCmd.ExecuteNonQuery();

                var delStatusesTableCmd = connection.CreateCommand();
                delStatusesTableCmd.CommandText = "DROP TABLE IF EXISTS statuses";
                delStatusesTableCmd.ExecuteNonQuery();

                SeedMeals();

                SeedStatuses();

                Display();




            }
        }

        private static void SeedMeals()
        {
            var createTableCmd = connection.CreateCommand();
            createTableCmd.CommandText = "CREATE TABLE meals(id INTEGER PRIMARY KEY, name VARCHAR(50), calories INTEGER, fat INTEGER, protein INTEGER)";
            createTableCmd.ExecuteNonQuery();

            //Seed some data:
            using (var transaction = connection.BeginTransaction())
            {
                var insertCmd = connection.CreateCommand();

                insertCmd.CommandText = "INSERT INTO meals VALUES(1, 'Butter Chicken', 100, 25, 20)";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO meals VALUES(2, 'Chicken Salad with Peanet Butter', 100, 25, 20)";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO meals VALUES(3, 'Poached Eggs with mashed potatoes', 100, 25, 20)";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO meals VALUES(4, 'Warm oatmeal with berries', 100, 25, 20)";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO meals VALUES(5, 'Quinoa Salad', 100, 25, 20)";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO meals VALUES(6, 'Salmon Wrap', 100, 25, 20)";
                insertCmd.ExecuteNonQuery();

                transaction.Commit();
            }
        }


        private static void SeedStatuses()
        {
            var createTableCmd = connection.CreateCommand();
            createTableCmd.CommandText = "CREATE TABLE statuses(name VARCHAR(50) PRIMARY KEY, message VARCHAR(50), success VARCHAR(50), failure VARCHAR(50))";
            createTableCmd.ExecuteNonQuery();

            //Seed some data:
            using (var transaction = connection.BeginTransaction())
            {
                var insertCmd = connection.CreateCommand();

                insertCmd.CommandText = "INSERT INTO statuses VALUES('WELCOME', 'Welcome to BENNs curbside pickup ordering platform', 'CALORIES', 'ERROR')";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO statuses VALUES('RESTART', 'Send any message to start creating a new order', 'WELCOME', 'WELCOME')";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO statuses VALUES('ERROR', 'Something went wrong, send any message to continue', 'RESTART', 'WELCOME')";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO statuses VALUES('CALORIES', 'Enter a value from 0 to 100 for the amount of calories', 'FATS', 'ERROR')";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO statuses VALUES('FATS', 'Enter a value from 0 to 100 for the amount of fats', 'PROTEINS', 'ERROR')";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO statuses VALUES('PROTEINS', 'Enter a value from 0 to 100 for the amount of proteins', 'MEAL_FOUND', 'MEAL_NOT_FOUND')";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO statuses VALUES('MEAL_FOUND', 'Here are the meals we found for the given criteria', 'CONFIRM_MEAL', 'ERROR')";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO statuses VALUES('MEAL_NOT_FOUND', 'We are sorry, we could not find any meals matching this criteria', 'RESTART', 'ERROR')";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO statuses VALUES('CONFIRM_MEAL', 'Are you sure this order is correct? ', 'PAYMENT', 'WELCOME')";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO statuses VALUES('PAYMENT', 'Click on this link to start payment for the order', 'PAY_SUCCESS', 'PAY_REJECT')";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO statuses VALUES('PAY_SUCCESS', 'Payment done succesfully', 'ORDER_SUCCESS', 'ERROR')";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO statuses VALUES('PAY_REJECT', 'Payment Rejected', 'ORDER_REJECT', 'ERROR')";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO statuses VALUES('ORDER_SUCCESS', 'Order placed succesfully', 'RESTART', 'ERROR')";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO statuses VALUES('ORDER_REJECT', 'Order declined', 'RESTART', 'ERROR')";
                insertCmd.ExecuteNonQuery();

                transaction.Commit();
            }
        }

        private static void Display()
        {
            //Read the newly inserted data:
            var selectMealCmd = connection.CreateCommand();
            selectMealCmd.CommandText = "SELECT name FROM meals";

            var selectStatusesCmd = connection.CreateCommand();
            selectStatusesCmd.CommandText = "SELECT name FROM statuses";

            using (var reader = selectMealCmd.ExecuteReader())
            {
                Console.WriteLine("MEALS");
                while (reader.Read())
                {
                    var message = reader.GetString(0);
                    Console.WriteLine(message);
                }
            }

            using (var reader = selectStatusesCmd.ExecuteReader())
            {
                Console.WriteLine("STATUSES");
                while (reader.Read())
                {
                    var message = reader.GetString(0);
                    Console.WriteLine(message);
                }
            }
        }
    }
}
