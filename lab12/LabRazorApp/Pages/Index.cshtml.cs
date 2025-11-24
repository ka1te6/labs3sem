using LabRazorApp.Data;
using LabRazorApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LabRazorApp.Pages;

public class IndexModel : PageModel
{
    private readonly LabContext _context;

    public IndexModel(LabContext context) => _context = context;

    public int ExperimentsCount { get; private set; }
    public int ResearchersCount { get; private set; }
    public int SamplesCount { get; private set; }
    public Experiment? LatestExperiment { get; private set; }
    public Sample? LatestSample { get; private set; }

    public async Task OnGetAsync()
    {
        ExperimentsCount = await _context.Experiments.CountAsync();
        ResearchersCount = await _context.Researchers.CountAsync();
        SamplesCount = await _context.Samples.CountAsync();

        LatestExperiment = await _context.Experiments
            .Include(e => e.PrincipalInvestigator)
            .OrderByDescending(e => e.StartDate)
            .FirstOrDefaultAsync();

        LatestSample = await _context.Samples
            .Include(s => s.Experiment)
            .OrderByDescending(s => s.CollectedDate)
            .FirstOrDefaultAsync();
    }
}
