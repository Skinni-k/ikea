using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace OrderBot
{
    public class Sqlite
    {
        // The Sqlite's constructor should always be private to prevent
        // direct construction calls with the `new` operator.
        private Sqlite()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "../SqliteDB.db";
            connection = new SqliteConnection(connectionStringBuilder.ConnectionString);
        }

        // The Sqlite's instance is stored in a static field. There there are
        // multiple ways to initialize this field, all of them have various pros
        // and cons. In this example we'll show the simplest of these ways,
        // which, however, doesn't work really well in multithreaded program.
        private static Sqlite sqlite;
        public static SqliteConnection connection;

        // This is the static method that controls the access to the Sqlite
        // instance. On the first run, it creates a Sqlite object and places
        // it into the static field. On subsequent runs, it returns the client
        // existing object stored in the static field.
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
