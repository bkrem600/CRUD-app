using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using UWS.Shared;

namespace Project.Pages
{
    public class CreateTrackModel : RelatedPageModel 
    {
        private Chinook db;

        public CreateTrackModel(Chinook injectedContext)
        {
            db = injectedContext;
        }

        public IActionResult OnGet()
        {
            PopulateAlbumsDropDownList(db);
            PopulateMediaTypesDropDownList(db);
            PopulateGenresDropDownList(db);
            return Page();
        }

        [BindProperty]
        public Track Track { get; set; }

        public async Task <IActionResult> OnPostAsync()
        {
            var emptyTrack = new Track();

            if (await TryUpdateModelAsync<Track>(
                 emptyTrack,
                 "track",   // Prefix for form value.
                 t => t.TrackId, t => t.AlbumId, t => t. Name, t => t.MediaTypeId, t => t.GenreId, t => t.Composer,
                t => t.Milliseconds, t => t.Bytes, t => t.UnitPrice))
            {
                db.Tracks.Add(emptyTrack);
                await db.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Select Ids if TryUpdateModelAsync fails.
            PopulateAlbumsDropDownList(db, emptyTrack.AlbumId);
            PopulateMediaTypesDropDownList(db, emptyTrack.MediaTypeId);
            PopulateGenresDropDownList(db, emptyTrack.GenreId);
            return Page();
        }
    }
}