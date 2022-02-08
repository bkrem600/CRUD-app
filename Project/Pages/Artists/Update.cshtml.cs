using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

using UWS.Shared;

namespace Project.Pages
{
    public class UpdateArtistModel : PageModel
    {
        private readonly Chinook _db;
        private readonly ILogger<UpdateArtistModel> _logger;
        public UpdateArtistModel (Chinook db, ILogger<UpdateArtistModel> logger)
        {
            _db = db;
            _logger = logger;
        }
        [BindProperty]
        public Artist Artist { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Artist = await _db.Artists.FindAsync(id);
            if (Artist == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var artistToUpdate = await _db.Artists.FindAsync(id);
            if (artistToUpdate == null)
            {
                return NotFound();
            }
            if (await TryUpdateModelAsync<Artist>(
                artistToUpdate,
                "artist",
                ar => ar.Name))
            {
                await _db.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}

