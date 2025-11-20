using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LabRazorApp.Data;
using LabRazorApp.Models;

namespace LabRazorApp.Pages.Researchers
{
    public class EditModel : PageModel
    {
        private readonly LabContext _context;

        public EditModel(LabContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Researcher Researcher { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Researcher = await _context.Researchers.FindAsync(id);

            if (Researcher == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _context.Attach(Researcher).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}

