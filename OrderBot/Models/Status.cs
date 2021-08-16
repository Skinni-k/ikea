using System;
using Microsoft.Data.Sqlite;

namespace OrderBot
{
    public class Status
    {
        public String name;
        public String message;
        public String success;
        public String failure;

        public String type;

        public Status(SqliteConnection connection, string query = "WELCOME")
        {
            this.GetState(query, connection);
        }

        private void GetState(string nCur, SqliteConnection connection)
        {
            string query = $"SELECT * FROM statuses where name='{nCur}'";
            this.executeSQL(query, connection);
        }

        private void executeSQL(String query, SqliteConnection connection)
        {
            var delMealsTableCmd = connection.CreateCommand();
            delMealsTableCmd.CommandText = query;
            var rdr = delMealsTableCmd.ExecuteReader();
            if (rdr.Read())
            {
                this.name = rdr.GetString(0);
                this.message = rdr.GetString(1);
                this.success = rdr.GetString(2);
                this.failure = rdr.GetString(3);
                this.type = rdr.GetString(4);
            }
        }

        public void nextState(SqliteConnection connection, Boolean success)
        {
            if (success) this.GetState(this.success, connection);
            else this.GetState(this.failure, connection);
        }

        public Boolean validValue(String input)
        {
            switch (this.type)
            {
                case "Number":
                    int num = 0;
                    return int.TryParse(input, out num);
                default:
                    return true;
            }
        }
    }
}
