using System.Collections.Generic;

namespace LinqAndEfOnSqLite
{
    // Represents a record in the Artist table
    public class Artist
    {
        public Artist()
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            Album = new List<Album>();
        }

        public long ArtistId { get; set; }
        public string Name { get; set; }

        // A record in the Album table will be related to the artist
        public virtual ICollection<Album> Album { get; set; }
    }
}
