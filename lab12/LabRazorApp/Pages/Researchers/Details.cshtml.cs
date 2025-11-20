using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LabRazorApp.Data;
using LabRazorApp.Models;

namespace LabRazorApp.Pages.Researchers
{
    public class DetailsModel : PageModel
    {
        private readonly LabContext _context;

        public DetailsModel(LabContext context)
        {
            _context = context;
        }

        public Researcher Researcher { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Researcher = await _context.Researchers.FindAsync(id);

            if (Researcher == null) return NotFound();

            return Page();
        }
    }
}

