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

        public IList<Experiment> Experiment { get; set; }

        public async Task OnGetAsync()
        {
            Experiment = await _context.Experiments
                .Include(e => e.PrincipalInvestigator)
                .ToListAsync();
        }
    }
}