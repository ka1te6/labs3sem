using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LabRazorApp.Data;
using LabRazorApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LabRazorApp.Pages.Samples
{
    public class EditModel : PageModel
    {
        private readonly LabContext _context;
        public EditModel(LabContext context) => _context = context;

        [BindProperty]
        public Sample Sample { get; set; }

        public SelectList ExperimentsList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Sample = await _context.Samples.FindAsync(id);
            if (Sample == null) return NotFound();

            ExperimentsList = new SelectList(
                await _context.Experiments.ToListAsync(),
                "Id",
                "Title"
            );

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync(Sample.Id);
                return Page();
            }

            _context.Attach(Sample).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}

