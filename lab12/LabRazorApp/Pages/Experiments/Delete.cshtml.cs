using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LabRazorApp.Data;
using LabRazorApp.Models;

namespace LabRazorApp.Pages.Experiments
{
    public class DeleteModel : PageModel
    {
        private readonly LabRazorApp.Data.LabContext _context;

        public DeleteModel(LabRazorApp.Data.LabContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Experiment Experiment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experiment = await _context.Experiments.FirstOrDefaultAsync(m => m.Id == id);

            if (experiment is not null)
            {
                Experiment = experiment;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experiment = await _context.Experiments.FindAsync(id);
            if (experiment != null)
            {
                Experiment = experiment;
                _context.Experiments.Remove(Experiment);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
