
using Microsoft.EntityFrameworkCore;

namespace MvcMusicStore.Models
{
    public class MusicStoreEntities : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        public MusicStoreEntities(DbContextOptions<MusicStoreEntities> opt) : base(opt) { }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>()
                .Property(a => a.Price)
                .HasPrecision(18,2);
        }
    }
}