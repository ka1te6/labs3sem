using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LabRazorApp.Data;
using LabRazorApp.Models;

namespace LabRazorApp.Pages.Researchers
{
    public class DeleteModel : PageModel
    {
        private readonly LabContext _context;

        public DeleteModel(LabContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            Researcher = await _context.Researchers.FindAsync(id);

            if (Researcher != null)
            {
                _context.Researchers.Remove(Researcher);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}

