using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LabRazorApp.Data;
using LabRazorApp.Models;

namespace LabRazorApp.Pages.Researchers
{
    public class IndexModel : PageModel
    {
        private readonly LabContext _context;

        public IndexModel(LabContext context)
        {
            _context = context;
        }

        public IList<Researcher> Researchers { get; set; }

        public async Task OnGetAsync()
        {
            Researchers = await _context.Researchers.ToListAsync();
        }
    }
}