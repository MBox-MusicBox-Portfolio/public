using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MBox.Models.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) => Database.EnsureCreated();

        public DbSet<User> Users { get; set; }
        public DbSet<Band> Bands { get; set; }
        public DbSet<MemberBand> Members { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<BandApplication> BandApplications { get; set; }
        public DbSet<ApplicationStatus> ApplicationStatuses { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationStatus>()
                .HasIndex(u => u.Name)
                .IsUnique();

            modelBuilder.Entity<Role>()
                .HasIndex(u => u.Name)
                .IsUnique();
        }
    }
}