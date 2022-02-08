using System.Collections.Generic;

namespace UWS.Shared
{
    public class Media_Type
    {
        public int MediaTypeId { get; set; }
        public string Name { get; set; }

        //related entities
        public ICollection<Track> Tracks { get; set; }    
    }
}