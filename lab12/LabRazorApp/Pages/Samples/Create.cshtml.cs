using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LabRazorApp.Data;
using LabRazorApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LabRazorApp.Pages.Samples
{
    public class CreateModel : PageModel
    {
        private readonly LabContext _context;
        public CreateModel(LabContext context) => _context = context;

        [BindProperty]
        public Sample Sample { get; set; }

        public SelectList ExperimentsList { get; set; }

        public async Task OnGetAsync()
        {
            ExperimentsList = new SelectList(
                await _context.Experiments.ToListAsync(),
                "Id",
                "Title"
            );
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            _context.Samples.Add(Sample);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}