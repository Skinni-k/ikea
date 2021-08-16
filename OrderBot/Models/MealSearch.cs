using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace OrderBot
{
    public class MealSearch
    {
        // public String calories;
        // public String proteins;
        // public String fats;

        public string selectedMeal;
        public string cost;
        public int calories = 0, fats = 0, proteins = 0;
        public List<string> meals = new List<string>();
        public List<int> mealsCost = new List<int>();


        public MealSearch() { }

        public Boolean searchMeal(SqliteConnection connection)
        {
            string query = $"SELECT * FROM meals where calories>{calories} and fats>{fats} and proteins>{proteins} LIMIT 3";
            this.executeSQL(query, connection);
            return this.meals.Count > 0;
        }

        private void executeSQL(String query, SqliteConnection connection)
        {
            var delMealsTableCmd = connection.CreateCommand();
            delMealsTableCmd.CommandText = query;
            var rdr = delMealsTableCmd.ExecuteReader();
            int i = 0;
            this.meals = new List<string>();
            while (rdr.Read() && i < 3)
            {
                this.meals.Add(rdr.GetString(1));
                this.mealsCost.Add(Int32.Parse(rdr.GetString(5)));
                i++;
            }
        }
    }
}
