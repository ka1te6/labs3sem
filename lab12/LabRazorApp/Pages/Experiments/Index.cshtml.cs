using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LabRazorApp.Data;
using LabRazorApp.Models;

namespace LabRazorApp.Pages.Experiments
{
    public class IndexModel : PageModel
    {
        private readonly LabContext _context;

        public IndexModel(LabContext context)
        {
            _context = context;
        }

        public IList<Experiment> Experiment { get; private set; } = new List<Experiment>();
        public int TotalCount { get; private set; }
        public int FilteredCount => Experiment.Count;

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Experiments
                .Include(e => e.PrincipalInvestigator)
                .Include(e => e.Samples)
                .AsQueryable();

            TotalCount = await query.CountAsync();

            if (!string.IsNullOrWhiteSpace(Search))
            {
                var term = $"%{Search.Trim()}%";
                query = query.Where(e =>
                    EF.Functions.Like(e.Title, term) ||
                    (e.Description != null && EF.Functions.Like(e.Description, term)) ||
                    (e.PrincipalInvestigator != null && EF.Functions.Like(e.PrincipalInvestigator.FullName, term)));
            }

            Experiment = await query
                .OrderByDescending(e => e.StartDate)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}