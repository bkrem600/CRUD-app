using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using UWS.Shared;

using Microsoft.AspNetCore.Mvc;

namespace Project.Pages
{
    public class ArtistDetailsModel : PageModel
    {
        private Chinook db;
        
        public ArtistDetailsModel(Chinook injectedContext)
        {
            db = injectedContext;
        }
        public Artist Artist { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Artist = await db.Artists
            .Include(ar => ar.Albums)
            .AsNoTracking()
            .FirstOrDefaultAsync(ar => ar.ArtistId == id);

             if (Artist == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}