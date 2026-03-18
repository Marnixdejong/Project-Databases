$conn = New-Object System.Data.SqlClient.SqlConnection
$conn.ConnectionString = "Server=tcp:project-databases.database.windows.net,1433;Initial Catalog=Databases_project;Encrypt=True;TrustServerCertificate=False;Authentication=`"Active Directory Default`";"
$conn.Open()
$cmd = $conn.CreateCommand()
$cmd.CommandText = "SELECT TABLE_NAME, COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME IN ('Rooms', 'Room', 'Lecturers', 'Lecturer', 'Students', 'Student', 'Persons', 'Person');"
$reader = $cmd.ExecuteReader()
while ($reader.Read()) {
    Write-Host "$($reader['TABLE_NAME']) - $($reader['COLUMN_NAME'])"
}
$conn.Close()
