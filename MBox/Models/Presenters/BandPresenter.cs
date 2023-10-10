using MBox.Models.Db;

namespace MBox.Models.Presenters
{
    public class BandPresenter
    {
        public BandPresenter(Band band)
        {
            Id = band.Id;
            Avatar = band.Avatar;
            Name = band.Name;
            Producer = band.Producer;
            CountMembers = band.Members.Count;
            CountSongs = band.Songs.Count;
            CreatedAt = band.CreatedAt;
            FullInfo = band.FullInfo;
            CountFollowers = band.Followers.Count;
            IsBlocked = band.IsBlocked;
            CountAlbums = band.Albums.Count;
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Avatar { get; set; }
        public string? Name { get; set; }
        public Producer? Producer { get; set; }
        public int CountMembers { get; set; }
        public int CountAlbums { get; set; }
        public int CountSongs { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public string? FullInfo { get; set; }
        public int CountFollowers { get; set; }
        public bool IsBlocked { get; set; } = false;
    }
}
