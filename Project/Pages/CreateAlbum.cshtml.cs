using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using UWS.Shared;

namespace Project.Pages
{
    public class CreateAlbumModel : ArtistPageModel
    {
        private Chinook db;

        public CreateAlbumModel(Chinook injectedContext)
        {
            db = injectedContext;
        }
        public IActionResult OnGet()
        {
            PopulateArtistsDropDownList(db);
            return Page();
        }

        [BindProperty]
        public Album Album { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyAlbum = new Album();

            if (await TryUpdateModelAsync<Album>(
                 emptyAlbum,
                 "album",   // Prefix for form value.
                 al => al.AlbumId, al => al.Title, al => al.ArtistId))
            {
                db.Albums.Add(emptyAlbum);
                await db.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Select ArtistId if TryUpdateModelAsync fails.
            PopulateArtistsDropDownList(db, emptyAlbum.ArtistId);
            return Page();
        }
    }
}
