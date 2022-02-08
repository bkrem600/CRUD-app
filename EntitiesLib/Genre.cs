using System.Collections.Generic;

namespace UWS.Shared
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }

        //related entities
        public ICollection<Track> Tracks { get; set; }    
    }
}