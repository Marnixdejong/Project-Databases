using System;
using Microsoft.Data.SqlClient;

class Program {
    static void Main() {
        string connString = "Server=tcp:project-databases.database.windows.net,1433;Initial Catalog=Databases_project;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\";";
        using (var conn = new SqlConnection(connString)) {
            conn.Open();
            Console.WriteLine("Connected!");
            var tables = new[] { "Person", "Student", "Lecturer", "Room", "Activity", "Building", "Drink", "Order" };
            foreach (var table in tables) {
                Console.WriteLine($"\nColumns for table: {table}");
                using (var cmd = new SqlCommand($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{table}'", conn)) {
                    using (var reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            Console.WriteLine($"- {reader[0]}");
                        }
                    }
                }
            }
        }
    }
}
