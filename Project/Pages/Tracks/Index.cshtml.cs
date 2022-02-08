using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;

using UWS.Shared;

namespace Project.Pages
{
    public class TracksModel : PageModel
    {
        public IList<Track> Tracks {get;set;}
        private Chinook db;
        public TracksModel(Chinook injectedContext)
        {
            db = injectedContext;
        }
        public string IdSort { get; set; }
        public string NameSort { get; set; }
        public string AlbumSort { get; set; }
        public string MediaSort { get; set; }
        public string GenreSort { get; set; }
        public string ComposerSort { get; set; }
        public string MillisecondsSort { get; set; }
        public string BytesSort { get; set; }
        public string PriceSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            IdSort = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            AlbumSort = String.IsNullOrEmpty(sortOrder) ? "album_desc" : "";
            MediaSort = String.IsNullOrEmpty(sortOrder) ? "media_desc" : "";
            GenreSort = String.IsNullOrEmpty(sortOrder) ? "genre_desc" : "";
            ComposerSort = String.IsNullOrEmpty(sortOrder) ? "composer_desc" : "";
            MillisecondsSort = String.IsNullOrEmpty(sortOrder) ? "milliseconds_desc" : "";
            BytesSort = String.IsNullOrEmpty(sortOrder) ? "bytes_desc" : "";

            CurrentFilter = searchString;

            IQueryable<Track> tracksIQ = from t in db.Tracks
                                        select t;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                tracksIQ = tracksIQ.Where(t => t.Name.ToUpper().Contains(searchString.ToUpper())
                                    || t.Album.Title.ToUpper().Contains(searchString.ToUpper())
                                    || t.Media_Type.Name.ToUpper().Contains(searchString.ToUpper())
                                    || t.Genre.Name.ToUpper().Contains(searchString.ToUpper())
                                    || t.Composer.ToUpper().Contains(searchString.ToUpper()));
            }

            switch(sortOrder)
            {
                case "id_desc":
                    tracksIQ = tracksIQ.OrderByDescending(t => t.TrackId);
                    break;
                case "name_desc":
                    tracksIQ = tracksIQ.OrderByDescending(t => t.Name);
                    break;
                case "album_desc":
                    tracksIQ = tracksIQ.OrderByDescending(t => t.Album.Title);
                    break;
                case "media_desc":
                    tracksIQ = tracksIQ.OrderByDescending(t => t.Media_Type.Name);
                    break;
                case "genre_desc":
                    tracksIQ = tracksIQ.OrderByDescending(t => t.Genre.Name);
                    break;
                case "composer_desc":
                    tracksIQ = tracksIQ.OrderByDescending(t => t.Composer);
                    break;
                case "milliseconds_desc":
                    tracksIQ = tracksIQ.OrderByDescending(t => t.Milliseconds);
                    break;
                case "bytes_desc":
                    tracksIQ = tracksIQ.OrderByDescending(t => t.Bytes);
                    break;
                default:
                    tracksIQ = tracksIQ.OrderBy(t => t.TrackId);
                    break;
            }

            Tracks = await tracksIQ
            .Include(t => t.Album)
            .Include(t => t.Media_Type)
            .Include(t => t.Genre)
            .AsNoTracking()
            .ToListAsync();
        }
    }
}