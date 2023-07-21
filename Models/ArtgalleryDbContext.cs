using Microsoft.EntityFrameworkCore;

namespace ArtGalleryOnline.Models
{
    public class ArtgalleryDbContext : DbContext
    {
        public ArtgalleryDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<ArtWork> ArtWork { get; set; }
        public DbSet<AuthorArtWork> AuthorArtWork { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<UserNotification> UserNotification { get; set; }
        public DbSet<Shipping> shippings { get; set; }
        public DbSet<ShippingFee> shippingFee { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Interest> Interest { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
