namespace AdministrationWebApi.Models.Db
{
    public class Playlist
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public User? User{ get; set; }
        public DateTime CreatePlaylist { get; set; }
        public List<Song> Songs { get; set; } = new();
    }
}
