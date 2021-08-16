using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace OrderBot
{
    public class Sqlite
    {
        private Sqlite()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "../SqliteDB.db";
            connection = new SqliteConnection(connectionStringBuilder.ConnectionString);
        }
        private static Sqlite sqlite;
        public static SqliteConnection connection;

        public static SqliteConnection GetConnection()
        {
            if (sqlite == null)
            {
                sqlite = new Sqlite();
                return connection;
            }
            return connection;
        }
    }
}
