using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace HalloFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            var sqlConString = "Server=.;Database=Northwind;Trusted_Connection=true";
            var sqliteConString = @"Data Source=C:\DB\northwind.sqlite";

            string conString;
            DbProviderFactory factory;
            if (!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!true)
            {
                factory = SqlClientFactory.Instance;
                conString = sqlConString;
            }
            else
            {
                factory = SQLiteFactory.Instance;
                conString = sqliteConString;
            }

            //using (DbConnection con = new SQLiteConnection(sqliteConString))
            using (DbConnection con = factory.CreateConnection())
            {
                con.ConnectionString = conString;
                con.Open();
                using (DbCommand cmd = factory.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Employees";
                    cmd.Connection = con;
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["FirstName"]} {reader["LastName"]} ");
                        }
                    }
                }
            }

            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
