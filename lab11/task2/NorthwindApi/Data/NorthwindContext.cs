using Microsoft.EntityFrameworkCore;
using NorthwindApi.Models;

namespace NorthwindApi.Data;

public class NorthwindContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost\\SQLEXPRESS01;Database=NorthwindLocal;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}
