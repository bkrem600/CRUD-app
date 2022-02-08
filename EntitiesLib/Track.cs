using System.Collections.Generic;
namespace UWS.Shared
{
    public class Track
    {
        public int TrackId { get; set; }
        public string Name { get; set; }
        public string Composer { get; set; }
        public int Milliseconds { get; set; }
        public int Bytes { get; set; }
        public decimal UnitPrice { get; set; }

        //related entities
        public Album Album { get; set; }
        public Media_Type Media_Type { get; set; }
        public Genre Genre { get; set; }
        public Playlist_Track Playlist_Track { get; set; }
        public ICollection<Invoice_Item> Invoice_Items { get; set; } 
        public int MediaTypeId { get; set; }
        public int GenreId { get; set; }
        public int AlbumId { get; set; }
    }
}