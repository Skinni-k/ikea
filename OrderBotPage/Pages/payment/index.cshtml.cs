using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderBot;

namespace wireless.Pages
{
    public class PaymentModel : PageModel
    {
        public string Meal;
        public int Cost;

        public void OnGet()
        {
            SqliteConnection connection = Sqlite.GetConnection();
            connection.Open();
            this.Meal = Request.Query["meal"];
            string query = $"SELECT * FROM meals where name='{Meal}' LIMIT 1";
            var delMealsTableCmd = connection.CreateCommand();
            delMealsTableCmd.CommandText = query;
            var rdr = delMealsTableCmd.ExecuteReader();
            rdr.Read();
            this.Cost = Int32.Parse(rdr.GetString(5));
        }
    }
}
