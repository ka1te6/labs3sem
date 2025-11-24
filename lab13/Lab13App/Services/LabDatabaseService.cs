using Lab13App.Data;
using Lab13App.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab13App.Services;

public class LabDatabaseService
{
    private readonly IDbContextFactory<LabDbContext> _contextFactory;
    private bool _initialized;

    public LabDatabaseService(IDbContextFactory<LabDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    private async Task EnsureInitializedAsync()
    {
        if (_initialized)
        {
            return;
        }

        await using var context = await _contextFactory.CreateDbContextAsync();
        await context.Database.EnsureCreatedAsync();
        _initialized = true;
    }

    public async Task<List<Researcher>> GetResearchersAsync()
    {
        await EnsureInitializedAsync();
        await using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Researchers
            .OrderBy(r => r.FullName)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Researcher> SaveResearcherAsync(Researcher researcher)
    {
        await EnsureInitializedAsync();
        await using var context = await _contextFactory.CreateDbContextAsync();

        if (researcher.Id == 0)
        {
            context.Researchers.Add(researcher);
        }
        else
        {
            context.Researchers.Update(researcher);
        }

        await context.SaveChangesAsync();
        return researcher;
    }

    public async Task DeleteResearcherAsync(int id)
    {
        await EnsureInitializedAsync();
        await using var context = await _contextFactory.CreateDbContextAsync();

        var entity = await context.Researchers.FindAsync(id);
        if (entity is null)
        {
            return;
        }

        context.Researchers.Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task<List<Experiment>> GetExperimentsAsync()
    {
        await EnsureInitializedAsync();
        await using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Experiments
            .Include(e => e.PrincipalInvestigator)
            .OrderBy(e => e.Title)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Experiment> SaveExperimentAsync(Experiment experiment)
    {
        await EnsureInitializedAsync();
        await using var context = await _contextFactory.CreateDbContextAsync();

        if (experiment.Id == 0)
        {
            context.Experiments.Add(experiment);
        }
        else
        {
            context.Experiments.Update(experiment);
        }

        await context.SaveChangesAsync();
        return experiment;
    }

    public async Task DeleteExperimentAsync(int id)
    {
        await EnsureInitializedAsync();
        await using var context = await _contextFactory.CreateDbContextAsync();

        var entity = await context.Experiments.FindAsync(id);
        if (entity is null)
        {
            return;
        }

        context.Experiments.Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task<List<Sample>> GetSamplesAsync()
    {
        await EnsureInitializedAsync();
        await using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Samples
            .Include(s => s.Experiment)
            .OrderBy(s => s.SampleCode)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Sample> SaveSampleAsync(Sample sample)
    {
        await EnsureInitializedAsync();
        await using var context = await _contextFactory.CreateDbContextAsync();

        if (sample.Id == 0)
        {
            context.Samples.Add(sample);
        }
        else
        {
            context.Samples.Update(sample);
        }

        await context.SaveChangesAsync();
        return sample;
    }

    public async Task DeleteSampleAsync(int id)
    {
        await EnsureInitializedAsync();
        await using var context = await _contextFactory.CreateDbContextAsync();

        var entity = await context.Samples.FindAsync(id);
        if (entity is null)
        {
            return;
        }

        context.Samples.Remove(entity);
        await context.SaveChangesAsync();
    }
}

