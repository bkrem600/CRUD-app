using System.Collections.Generic;

namespace UWS.Shared
{
    public class Artist
    {
        public int ArtistId { get; set; }
        public string Name { get; set; } 

        //related entities
        public ICollection<Album> Albums { get; set; }       
    }
}