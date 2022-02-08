namespace UWS.Shared
{
    public class Playlist_Track
    {
        public int PlaylistId { get; set; }
        public int TrackId { get; set; }
        //related entities
        public Playlist Playlist { get; set; }
        public Track Track { get; set; }        
    }
}