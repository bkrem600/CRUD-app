using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UWS.Shared;

namespace Project.Pages
{
    public class GenresModel : PageModel
    {
        private Chinook db;
        public GenresModel(Chinook injectedContext)
        {
            db = injectedContext;
        }
        public IList<Genre> Genres { get; set; }
        public void OnGet() 
        {
            Genres = db.Genres.ToList();            
        }
    }
}