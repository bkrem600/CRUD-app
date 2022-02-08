using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using UWS.Shared;

namespace Project.Pages
{
    public class ArtistPageModel : PageModel
    {
        public SelectList ArtistNameSL { get; set; }  

        public void PopulateArtistsDropDownList(Chinook db,
            object selectedArtist = null)
        {
            var artistsQuery = from ar in db.Artists
                                   orderby ar.Name // Sort by name.
                                   select ar;

            ArtistNameSL = new SelectList(artistsQuery.AsNoTracking(),
                        "ArtistId", "Name", selectedArtist);
        }
    }
}