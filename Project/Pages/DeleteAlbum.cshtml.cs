using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;

using UWS.Shared;

namespace Project.Pages
{
    public class DeleteAlbumModel : PageModel
    {
        private readonly Chinook _db;
        
        public DeleteAlbumModel (Chinook db)
        {
            _db = db;
        }
        [BindProperty]
        public Album Album { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Album = await _db.Albums.FirstOrDefaultAsync(al => al.AlbumId == id);
            
            if (Album == null)
            {
                return NotFound();
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Album album = await _db.Albums
                        .Include(al => al.Tracks)
                        .SingleAsync(al => al.AlbumId == id);
            
            if (album == null)
            {
                return RedirectToPage("./Index");
            }

            _db.Albums.Remove(album);
            await _db.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}

