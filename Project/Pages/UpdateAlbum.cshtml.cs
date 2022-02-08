using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using UWS.Shared;

namespace Project.Pages
{
    public class UpdateAlbumModel : ArtistPageModel
    {
        private Chinook db;
        
        public UpdateAlbumModel (Chinook injectedContext)
        {
            db = injectedContext;
        }

        [BindProperty]
        public Album Album { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            PopulateArtistsDropDownList(db);
            
            if (id == null)
            {
                return NotFound();
            }
            Album = await db.Albums.FindAsync(id);
            if (Album == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var albumToUpdate = await db.Albums
            .FindAsync(id);
            if (albumToUpdate == null)
            {
                return NotFound();
            }
            if (await TryUpdateModelAsync<Album>(
                albumToUpdate,
                "album",
                al => al.Title, al => al.ArtistId))
            {
                await db.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            // Select ArtistId if TryUpdateModelAsync fails.
            PopulateArtistsDropDownList(db, albumToUpdate.ArtistId);
            return Page();
        }
    }
}

