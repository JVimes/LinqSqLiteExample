namespace LinqAndEfOnSqLite
{
    // Represents a record in the Album table
    public class Album
    {
        public long AlbumId { get; set; }
        public string Title { get; set; }

        public long ArtistId { get; set; }
        public virtual Artist Artist { get; set; }
    }
}
