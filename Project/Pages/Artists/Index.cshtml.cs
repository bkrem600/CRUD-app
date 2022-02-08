using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using UWS.Shared;

namespace Project.Pages
{
    public class ArtistsModel : PageModel
    {
        private Chinook db;
        
        public ArtistsModel(Chinook injectedContext)
        {
            db = injectedContext;
        }
        public string NameSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public IList<Artist> Artists { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString) 
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            CurrentFilter = searchString;
            IQueryable<Artist> artistsIQ = from ar in db.Artists
                                        select ar;
            if (!String.IsNullOrEmpty(searchString))
            {
                artistsIQ = artistsIQ.Where(ar => ar.Name.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder) 
            {
                case "name_desc":
                    artistsIQ = artistsIQ.OrderByDescending(ar => ar.Name);
                    break;
                default:
                    artistsIQ = artistsIQ.OrderBy(ar => ar.ArtistId);
                    break;
            }
            
        Artists = await artistsIQ.AsNoTracking().ToListAsync();
        }
        
        [BindProperty]
        public Artist Artist { get; set; }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                db.Artists.Add(Artist);
                db.SaveChanges();
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}