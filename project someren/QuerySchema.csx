#r "nuget: Microsoft.Data.SqlClient, 5.1.5"
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System;

var connectionString = "Server=tcp:project-databases.database.windows.net,1433;Initial Catalog=Databases_project;Encrypt=True;TrustServerCertificate=False;Authentication=\"Active Directory Default\";";
using var connection = new SqlConnection(connectionString);
connection.Open();

using var command = new SqlCommand("SELECT TABLE_NAME, COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME IN ('Rooms', 'Room', 'Lecturers', 'Lecturer', 'Students', 'Student', 'Persons', 'Person');", connection);
using var reader = command.ExecuteReader();
while (reader.Read())
{
    Console.WriteLine($"{reader["TABLE_NAME"]} - {reader["COLUMN_NAME"]}");
}
