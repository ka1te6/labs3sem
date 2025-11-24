using Lab13App.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab13App.Services;

public class DatabaseInitializer
{
    private readonly IDbContextFactory<LabDbContext> _factory;

    public DatabaseInitializer(IDbContextFactory<LabDbContext> factory)
    {
        _factory = factory;
    }

    public async Task InitializeAsync()
    {
        await using var context = await _factory.CreateDbContextAsync();
        await context.Database.EnsureCreatedAsync();
    }
}

