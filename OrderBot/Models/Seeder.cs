using System;
using Microsoft.Data.Sqlite;

namespace OrderBot
{
    public static class Seeder
    {
        private static SqliteConnection connection;

        public static void Seed()
        {
            connection = Sqlite.GetConnection();
            connection.Open();
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
            createTableCmd.CommandText = "CREATE TABLE meals(id INTEGER PRIMARY KEY, name VARCHAR(50), calories INTEGER, fats INTEGER, proteins INTEGER, cost INTEGER)";
            createTableCmd.ExecuteNonQuery();

            //Seed some data:
            using (var transaction = connection.BeginTransaction())
            {
                var insertCmd = connection.CreateCommand();

                insertCmd.CommandText = "INSERT INTO meals VALUES(1, 'Butter Chicken', 100, 25, 20, 100)";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO meals VALUES(2, 'Chicken Salad with Peanet Butter', 100, 25, 20, 150)";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO meals VALUES(3, 'Poached Eggs with mashed potatoes', 100, 25, 20, 200)";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO meals VALUES(4, 'Warm oatmeal with berries', 100, 25, 20, 50)";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO meals VALUES(5, 'Quinoa Salad', 100, 25, 20, 40)";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO meals VALUES(6, 'Salmon Wrap', 100, 25, 20, 30)";
                insertCmd.ExecuteNonQuery();

                transaction.Commit();
            }
        }


        private static void SeedStatuses()
        {
            var createTableCmd = connection.CreateCommand();
            createTableCmd.CommandText = "CREATE TABLE statuses(name VARCHAR(50) PRIMARY KEY, message VARCHAR(50), success VARCHAR(50), failure VARCHAR(50), type VARCHAR(50))";
            createTableCmd.ExecuteNonQuery();

            //Seed some data:
            using (var transaction = connection.BeginTransaction())
            {
                var insertCmd = connection.CreateCommand();

                insertCmd.CommandText = "INSERT INTO statuses VALUES('WELCOME', 'Hi!, Welcome to BENNs curbside pickup ordering platform. \n To choose a meal you need to input the amount of calories, fats and proteins you want in your meal.\nSend any message to start creating the order', 'CALORIES', 'ERROR', 'String')";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO statuses VALUES('RESTART', 'Send any message to start creating a new order', 'CALORIES', 'WELCOME', 'String')";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO statuses VALUES('ERROR', 'Something went wrong, send any message to continue', 'RESTART', 'WELCOME', 'String')";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO statuses VALUES('CALORIES', 'Enter a value from 0 to 100 for the amount of calories', 'FATS', 'ERROR', 'Number')";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO statuses VALUES('FATS', 'Enter a value from 0 to 100 for the amount of fats', 'PROTEINS', 'ERROR', 'Number')";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO statuses VALUES('PROTEINS', 'Enter a value from 0 to 100 for the amount of proteins', 'MEAL_FOUND', 'MEAL_NOT_FOUND', 'Number')";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO statuses VALUES('MEAL_FOUND', 'Here are the meals we found for the given criteria:\n', 'CONFIRM_MEAL', 'MEAL_FOUND', 'Number')";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO statuses VALUES('MEAL_NOT_FOUND', 'We are sorry, we could not find any meals matching this criteria. Send any message to continue.', 'RESTART', 'ERROR', 'String')";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO statuses VALUES('CONFIRM_MEAL', 'Please type Yes if this order is correct: ', 'PAYMENT', 'WELCOME', 'Boolean')";
                insertCmd.ExecuteNonQuery();

                insertCmd.CommandText = "INSERT INTO statuses VALUES('PAYMENT', 'Click on this link to start payment for the order: ', 'PAY_SUCCESS', 'PAY_REJECT', 'String')";
                insertCmd.ExecuteNonQuery();

                // insertCmd.CommandText = "INSERT INTO statuses VALUES('PAY_SUCCESS', 'Payment done succesfully. Send any message to continue.', 'ORDER_SUCCESS', 'ERROR', 'String')";
                // insertCmd.ExecuteNonQuery();

                // insertCmd.CommandText = "INSERT INTO statuses VALUES('PAY_REJECT', 'Payment Rejected. Send any message to continue.', 'ORDER_REJECT', 'ERROR', 'String')";
                // insertCmd.ExecuteNonQuery();

                // insertCmd.CommandText = "INSERT INTO statuses VALUES('ORDER_SUCCESS', 'Order placed succesfully. Send any message to continue.', 'RESTART', 'ERROR', 'String')";
                // insertCmd.ExecuteNonQuery();

                // insertCmd.CommandText = "INSERT INTO statuses VALUES('ORDER_REJECT', 'Order declined. Send any message to continue.', 'RESTART', 'ERROR', 'String')";
                // insertCmd.ExecuteNonQuery();

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
