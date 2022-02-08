using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

using System.Linq;
using UWS.Shared;

namespace Project.Pages
{
    public class AlbumsModel : PageModel
    {
        private Chinook db;
        public AlbumsModel(Chinook injectedContext)
        {
            db = injectedContext;
        }
        public string TitleSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public IList<Album> Albums { get; set; }
    
        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            TitleSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            CurrentFilter = searchString;
            IQueryable<Album> albumsIQ = from al in db.Albums
                                        select al;
            if (!String.IsNullOrEmpty(searchString))
            {
                albumsIQ = albumsIQ.Where(al => al.Title.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
        {
            case "name_desc":
                albumsIQ = albumsIQ.OrderByDescending(al => al.Title);
                break;
            default:
                albumsIQ = albumsIQ.OrderBy(al => al.Title);
                break;
        }

        Albums = await albumsIQ.AsNoTracking().ToListAsync();
        }
    }
}