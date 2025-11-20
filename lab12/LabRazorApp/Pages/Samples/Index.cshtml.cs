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

        public IList<Sample> Samples { get; set; }

        public async Task OnGetAsync()
        {
            Samples = await _context.Samples
                .Include(s => s.Experiment)
                .ToListAsync();
        }
    }
}

