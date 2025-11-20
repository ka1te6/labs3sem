using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LabRazorApp.Data;
using LabRazorApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LabRazorApp.Pages.Samples
{
    public class DeleteModel : PageModel
    {
        private readonly LabContext _context;
        public DeleteModel(LabContext context) => _context = context;

        [BindProperty]
        public Sample Sample { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Sample = await _context.Samples
                .Include(s => s.Experiment)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (Sample == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            var sample = await _context.Samples.FindAsync(id);

            if (sample != null)
            {
                _context.Samples.Remove(sample);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}

