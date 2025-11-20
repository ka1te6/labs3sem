using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LabRazorApp.Data;
using LabRazorApp.Models;

namespace LabRazorApp.Pages_Samples
{
    public class DetailsModel : PageModel
    {
        private readonly LabRazorApp.Data.LabContext _context;

        public DetailsModel(LabRazorApp.Data.LabContext context)
        {
            _context = context;
        }

        public Sample Sample { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sample = await _context.Samples.FirstOrDefaultAsync(m => m.Id == id);

            if (sample is not null)
            {
                Sample = sample;

                return Page();
            }

            return NotFound();
        }
    }
}
