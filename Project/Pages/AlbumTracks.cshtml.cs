using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using UWS.Shared;

using Microsoft.AspNetCore.Mvc;

namespace Project.Pages
{
    public class AlbumTracksModel : PageModel
    {
        private Chinook db;
        
        public AlbumTracksModel(Chinook injectedContext)
        {
            db = injectedContext;
        }
        public Album Album { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Album = await db.Albums
            .Include(al => al.Tracks)
            .AsNoTracking()
            .FirstOrDefaultAsync(al => al.AlbumId == id);

             if (Album == null)
            {
                return NotFound();
            }
            return Page();
        }

        
    }
}