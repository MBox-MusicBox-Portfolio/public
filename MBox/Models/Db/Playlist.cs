namespace MBox.Models.Db
{
    public class Playlist
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public User Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Song> Songs { get; set; }  
    }
}
