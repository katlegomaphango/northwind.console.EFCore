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


WriteLine("Authenticate using:");
WriteLine(" 1 - Windows Integrated Security");
WriteLine(" 2 - SQL Login, for example, sa");
WriteLine();
Write("Press a key: ");

key = ReadKey().Key;
WriteLine();
WriteLine();


if (key is ConsoleKey.D1 or ConsoleKey.NumPad1)
{
    builder.IntegratedSecurity = true;
} else if(key is ConsoleKey.D2 or ConsoleKey.NumPad2)
{
    builder.UserID = "sa";

    Write("Enter your SQL Server password: ");
    string? password = ReadLine();

    if (string.IsNullOrWhiteSpace(password))
    {
        WriteLine("Password cannot be empty or null.");
        return;
    }
    builder.Password = password;
    builder.PersistSecurityInfo = false;
}
else
{
    WriteLine("No authentication selected.");
    return;
}


