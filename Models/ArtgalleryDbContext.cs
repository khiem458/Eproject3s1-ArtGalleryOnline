using Microsoft.EntityFrameworkCore;

namespace ArtGalleryOnline.Models
{
    public class ArtgalleryDbContext : DbContext
    {
        public ArtgalleryDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<ArtWork> ArtWorks { get; set; }
        public DbSet<AuthorArtWork> AuthorArtWork { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<Shipping> shippings { get; set; }
        public DbSet<ShippingFee> shippingFees { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<Category> Categories { get; set; }
        

    }
}
