using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LabRazorApp.Data;
using LabRazorApp.Models;

namespace LabRazorApp.Pages.Experiments
{
    public class DetailsModel : PageModel
    {
        private readonly LabContext _context;

        public DetailsModel(LabContext context)
        {
            _context = context;
        }

        public Experiment Experiment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Experiment = await _context.Experiments
                .Include(e => e.PrincipalInvestigator)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Experiment == null) return NotFound();

            return Page();
        }
    }
}


