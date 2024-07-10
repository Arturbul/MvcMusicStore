
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
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>()
                .Property(a => a.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(a => a.Total)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderDetail>()
                .Property(a => a.UnitPrice)
                .HasPrecision(18, 2);
        }
    }
}