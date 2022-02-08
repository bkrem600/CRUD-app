using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using UWS.Shared;

using Microsoft.AspNetCore.Mvc;

namespace Project.Pages
{
    public class AlbumDetailsModel : PageModel
    {
        private Chinook db;
        
        public AlbumDetailsModel(Chinook injectedContext)
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
            .Include(al => al.Artist)
            .Include(al => al.Tracks)
            .ThenInclude(t => t.Media_Type)
            .Include(al => al.Tracks)
            .ThenInclude(t => t.Genre)
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