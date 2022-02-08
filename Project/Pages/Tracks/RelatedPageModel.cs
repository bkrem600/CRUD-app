using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using UWS.Shared;

namespace Project.Pages
{
    public class RelatedPageModel : PageModel
    {
        public SelectList AlbumTitleSL { get; set; }
        public SelectList MediaTypeNameSL { get; set; }
        public SelectList GenreNameSL { get; set; }   

        public void PopulateAlbumsDropDownList(Chinook db,
            object selectedAlbum = null)
        {
            var albumsQuery = from al in db.Albums
                                   orderby al.Title // Sort by title.
                                   select al;

            AlbumTitleSL = new SelectList(albumsQuery.AsNoTracking(),
                        "AlbumId", "Title", selectedAlbum);
        }

        public void PopulateMediaTypesDropDownList(Chinook db,
            object selectedMediaType = null)
        {
            var mediaTypesQuery = from mt in db.Media_Types
                                   orderby mt.Name // Sort by name.
                                   select mt;

            MediaTypeNameSL = new SelectList(mediaTypesQuery.AsNoTracking(),
                        "MediaTypeId", "Name", selectedMediaType);
        }

        public void PopulateGenresDropDownList(Chinook db,
            object selectedGenre = null)
        {
            var genresQuery = from g in db.Genres
                                   orderby g.Name // Sort by name.
                                   select g;

            GenreNameSL = new SelectList(genresQuery.AsNoTracking(),
                        "GenreId", "Name", selectedGenre);
        }
    }
}