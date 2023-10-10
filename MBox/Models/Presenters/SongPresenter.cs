using MBox.Models.Db;

namespace MBox.Models.Presenters
{
    public class SongPresenter
    {
        public SongPresenter(Song song)
        {
            Id = song.Id;
            Poster = song.Poster;
            Name = song.Name;
            Performer = song.Performer;
            Description = song.Description;
            IsBlock = song.IsBlock;
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Poster { get; set; }
        public string? Name { get; set; }
        public List<Band>? Performer { get; set; }
        public string? Description { get; set; }
        public bool IsBlock { get; set; }
    }
}
