using System.Collections.Generic;

namespace UWS.Shared
{
    public class Album
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public int ArtistId { get; set; }
        
        //related entities
        public Artist Artist { get; set; }
        public ICollection<Track> Tracks { get; set; }
    }
}
