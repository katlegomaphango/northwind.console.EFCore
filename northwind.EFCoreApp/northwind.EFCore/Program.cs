﻿//Scaffold - DbContext "Data Source=.;Database=Northwind;TrustServerCertificate=True;Trusted_Connection=True;"
//-provider Microsoft.EntityFrameworkCore.SqlServer - OutputDir Models - DataAnnotations - context NorthwindDb

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using northwind.EFCore.Models;

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

DbContextOptionsBuilder<NorthwindDb> options = new();
options.UseSqlServer(builder.ConnectionString);

using (NorthwindDb db = new(options.Options))
{
    Write("Enter a unit price: ");
    string? priceText = ReadLine();

    if (!decimal.TryParse(priceText, out decimal price))
    {
        WriteLine("You must enter a valid unit price.");
        return;
    }

    var products = db.Products
        .Where(p => p.UnitPrice > price)
        .Select(p => new { p.ProductId, p.ProductName, p.UnitPrice });

    WriteLine("----------------------------------------------------------");
    WriteLine("| {0,5} | {1,-35} | {2,8} |", "Id", "Name", "Price");
    WriteLine("----------------------------------------------------------");

    foreach (var p in products)
    {
        WriteLine("| {0,5} | {1,-35} | {2,8:C} |",
        p.ProductId, p.ProductName, p.UnitPrice);
    }

    WriteLine("----------------------------------------------------------");

    WriteLine(products.ToQueryString());
    WriteLine();
    WriteLine($"Provider: {db.Database.ProviderName}");
    WriteLine($"Connection: {db.Database.GetConnectionString()}");

}
