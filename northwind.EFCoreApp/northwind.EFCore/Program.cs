//Scaffold - DbContext "Data Source=.;Database=Northwind;TrustServerCertificate=True;Trusted_Connection=True;"
//-provider Microsoft.EntityFrameworkCore.SqlServer - OutputDir Models - DataAnnotations - context NorthwindDb

using Microsoft.Data.SqlClient;

SqlConnectionStringBuilder builder = new();

builder.InitialCatalog = "Northwind";
builder.MultipleActiveResultSets = true;
builder.Encrypt = true;
builder.TrustServerCertificate = true;
builder.ConnectTimeout = 10;

WriteLine("Connect to:");
WriteLine(" 1 - SQL Server on local machine");
WriteLine(" 2 - Azure SQL Database");
WriteLine(" 3 - Azure SQL Edge");
WriteLine();
Write("Press a key: ");

ConsoleKey key = ReadKey().Key;
WriteLine(); 
WriteLine();

if (key is ConsoleKey.D1 or ConsoleKey.NumPad1)
{
    builder.DataSource = ".";
}
else if (key is ConsoleKey.D2 or ConsoleKey.NumPad2)
{
    builder.DataSource = "tcp:apps-services-net7.database.windows.net,1433";
}
else if (key is ConsoleKey.D3 or ConsoleKey.NumPad3)
{
    builder.DataSource = "tcp:127.0.0.1,1433";
}
else
{
    WriteLine("No data source selected.");
    return;
}