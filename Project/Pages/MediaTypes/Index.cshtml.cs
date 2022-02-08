using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UWS.Shared;

namespace Project.Pages
{
    public class MediaTypesModel : PageModel
    {
        private Chinook db;
        public MediaTypesModel(Chinook injectedContext)
        {
            db = injectedContext;
        }
        public IList<Media_Type> MediaTypes { get; set; }
        public void OnGet() 
        {
            MediaTypes = db.Media_Types.ToList();            
        }
    }
}