using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabRazorApp.Data;
using LabRazorApp.Models;

namespace LabRazorApp.Pages.Experiments
{
    public class EditModel : PageModel
    {
        private readonly LabContext _context;

        public EditModel(LabContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Experiment Experiment { get; set; }

        public SelectList ResearchersList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Experiment = await _context.Experiments.FirstOrDefaultAsync(m => m.Id == id);

            if (Experiment == null) return NotFound();

            ResearchersList = new SelectList(
                await _context.Researchers.ToListAsync(),
                "Id",
                "FullName",
                Experiment.PrincipalInvestigatorId
            );

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ResearchersList = new SelectList(
                    await _context.Researchers.ToListAsync(),
                    "Id",
                    "FullName",
                    Experiment.PrincipalInvestigatorId
                );
                return Page();
            }

            _context.Attach(Experiment).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

