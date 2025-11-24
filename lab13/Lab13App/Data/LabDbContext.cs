using Lab13App.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab13App.Data;

public class LabDbContext : DbContext
{
    public LabDbContext(DbContextOptions<LabDbContext> options) : base(options)
    {
    }

    public DbSet<Researcher> Researchers => Set<Researcher>();
    public DbSet<Experiment> Experiments => Set<Experiment>();
    public DbSet<Sample> Samples => Set<Sample>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Sample>()
            .HasIndex(s => s.SampleCode)
            .IsUnique();

        modelBuilder.Entity<Researcher>().HasData(
            new Researcher { Id = 1, FullName = "Иванов И.И.", Position = "PI", Email = "ivanov@lab.org" },
            new Researcher { Id = 2, FullName = "Петров П.П.", Position = "Postdoc", Email = "petrov@lab.org" }
        );

        modelBuilder.Entity<Experiment>().HasData(
            new Experiment
            {
                Id = 1,
                Title = "Катализация A",
                Description = "Исследование каталитической активности",
                StartDate = new DateTime(2025, 1, 10),
                PrincipalInvestigatorId = 1
            },
            new Experiment
            {
                Id = 2,
                Title = "Стабильность B",
                Description = "Проверка стабильности при 37°C",
                StartDate = new DateTime(2025, 2, 1),
                PrincipalInvestigatorId = 2
            }
        );

        modelBuilder.Entity<Sample>().HasData(
            new Sample
            {
                Id = 1,
                SampleCode = "S-2025-0001",
                Type = "tissue",
                CollectedDate = new DateTime(2025, 1, 11),
                Status = "Collected",
                ExperimentId = 1
            },
            new Sample
            {
                Id = 2,
                SampleCode = "S-2025-0002",
                Type = "solution",
                CollectedDate = new DateTime(2025, 2, 2),
                Status = "Processed",
                ExperimentId = 2
            }
        );
    }
}

