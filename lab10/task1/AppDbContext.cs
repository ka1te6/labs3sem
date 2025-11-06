using Microsoft.EntityFrameworkCore;
using System;

public class AppDbContext : DbContext
{
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<TodaysCondition> Conditions { get; set; }
    public DbSet<Price> Prices { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString =
            "Server=localhost\\SQLEXPRESS01;Database=StocksDb;Trusted_Connection=True;TrustServerCertificate=True;";

        optionsBuilder.UseSqlServer(connectionString, options =>
        {
            options.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null);
        });
    }
}