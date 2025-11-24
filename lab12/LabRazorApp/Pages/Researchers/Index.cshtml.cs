using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LabRazorApp.Data;
using LabRazorApp.Models;

namespace LabRazorApp.Pages.Researchers
{
    public class IndexModel : PageModel
    {
        private readonly LabContext _context;

        public IndexModel(LabContext context)
        {
            _context = context;
        }

        public IList<Researcher> Researchers { get; private set; } = new List<Researcher>();
        public int TotalCount { get; private set; }
        public int FilteredCount => Researchers.Count;

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Researchers
                .Include(r => r.Experiments)
                .AsQueryable();

            TotalCount = await query.CountAsync();

            if (!string.IsNullOrWhiteSpace(Search))
            {
                var term = $"%{Search.Trim()}%";
                query = query.Where(r =>
                    EF.Functions.Like(r.FullName, term) ||
                    (r.Position != null && EF.Functions.Like(r.Position, term)) ||
                    (r.Email != null && EF.Functions.Like(r.Email, term)));
            }

            Researchers = await query
                .OrderBy(r => r.FullName)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}