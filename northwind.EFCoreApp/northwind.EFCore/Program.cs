//Scaffold - DbContext "Data Source=.;Database=Northwind;TrustServerCertificate=True;Trusted_Connection=True;"
//-provider Microsoft.EntityFrameworkCore.SqlServer - OutputDir Models - DataAnnotations - context NorthwindDb

using Microsoft.Data.SqlClient;

SqlConnectionStringBuilder builder = new();

builder.InitialCatalog = "Northwind";
builder.MultipleActiveResultSets = true;
builder.Encrypt = true;
builder.TrustServerCertificate = true;
builder.ConnectTimeout = 10;
