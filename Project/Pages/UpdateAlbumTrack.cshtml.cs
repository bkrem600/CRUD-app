using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using UWS.Shared;

namespace Project.Pages
{
    public class UpdateAlbumTrackModel : RelatedPageModel
    {
        private Chinook db;
        
        public UpdateAlbumTrackModel (Chinook injectedContext)
        {
            db = injectedContext;
            
        }
        
        [BindProperty]
        public Track Track { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            PopulateAlbumsDropDownList(db);
            PopulateMediaTypesDropDownList(db);
            PopulateGenresDropDownList(db);

            if (id == null)
            {
                return NotFound();
            }
            Track = await db.Tracks.FindAsync(id);
            if (Track == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var trackToUpdate = await db.Tracks.FindAsync(id);
            if (trackToUpdate == null)
            {
                return NotFound();
            }
            if (await TryUpdateModelAsync<Track>(
                trackToUpdate,
                "track",
                t => t.Name, t => t.AlbumId, t => t.MediaTypeId, t => t.GenreId, t => t.Composer,
                t => t.Milliseconds, t => t.Bytes, t => t.UnitPrice))
            {
                await db.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            // Select Ids if TryUpdateModelAsync fails.
            PopulateAlbumsDropDownList(db, trackToUpdate.AlbumId);
            PopulateMediaTypesDropDownList(db, trackToUpdate.MediaTypeId);
            PopulateGenresDropDownList(db, trackToUpdate.GenreId);

            return Page();
        }
    }
}

