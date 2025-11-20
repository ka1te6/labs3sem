using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LabRazorApp.Data;
using LabRazorApp.Models;

namespace LabRazorApp.Pages.Researchers
{
    public class CreateModel : PageModel
    {
        private readonly LabContext _context;

        public CreateModel(LabContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Researcher Researcher { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Researchers.Add(Researcher);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}

