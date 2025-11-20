using LabRazorApp.Data;
using LabRazorApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LabRazorApp.Pages.Experiments
{
    public class CreateModel : PageModel
    {
        private readonly LabContext _context;

        public CreateModel(LabContext context) 
        { 
            _context = context; 
        }

        [BindProperty]
        public Experiment Experiment { get; set; } = new();

        public SelectList? ResearchersList { get; set; }

        public IActionResult OnGet()
        {
            PopulateResearchersDropDownList();
            return Page();
        }

        private void PopulateResearchersDropDownList(object? selected = null)
        {
            var q = _context.Researchers
                .OrderBy(r => r.FullName)
                .AsNoTracking();

            ResearchersList = new SelectList(q, "Id", "FullName", selected);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateResearchersDropDownList(Experiment.PrincipalInvestigatorId);
                return Page();
            }

            _context.Experiments.Add(Experiment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}