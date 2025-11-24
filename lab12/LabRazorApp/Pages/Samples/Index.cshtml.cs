using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LabRazorApp.Data;
using LabRazorApp.Models;

namespace LabRazorApp.Pages.Samples
{
    public class IndexModel : PageModel
    {
        private readonly LabContext _context;
        public IndexModel(LabContext context) => _context = context;

        public IList<Sample> Samples { get; private set; } = new List<Sample>();
        public int TotalCount { get; private set; }
        public int FilteredCount => Samples.Count;

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Samples
                .Include(s => s.Experiment)
                .AsQueryable();

            TotalCount = await query.CountAsync();

            if (!string.IsNullOrWhiteSpace(Search))
            {
                var term = $"%{Search.Trim()}%";
                query = query.Where(s =>
                    EF.Functions.Like(s.SampleCode, term) ||
                    (s.Status != null && EF.Functions.Like(s.Status, term)) ||
                    (s.Type != null && EF.Functions.Like(s.Type, term)) ||
                    (s.Experiment != null && EF.Functions.Like(s.Experiment.Title, term)));
            }

            Samples = await query
                .OrderByDescending(s => s.CollectedDate)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}

