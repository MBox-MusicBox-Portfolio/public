using MBox.Models.Db;

namespace MBox.Models.Presenters
{
    public class AlbumPresenter
    {
        public AlbumPresenter(Album album)
        {
            Id = album.Id;
            Poster = album.Poster;
            Name = album.Name;
            Author = album.Band;
            CountSongs = album.Songs.Count;
            ReleaseDate = album.Release;
            Description = album.Description;
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Poster { get; set; }
        public string? Name { get; set; }
        public Band? Author { get; set; }
        public int CountSongs { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Description { get; set; }
    }
}
