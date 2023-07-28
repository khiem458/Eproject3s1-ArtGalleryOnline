using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ArtGalleryOnline.Models
{
    public enum Gender
    {
        Male = 0,
        Female = 1
    }
    public enum UserRole
    {
        admin = 3,
        user = 1,
        author = 2
    }
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        [StringLength(50)]
        public string? UserName { get; set; }
        [StringLength(50)]
        public string? UserFullName { get; set; }
        [Required]
        [StringLength(50)]
        public string? UserEmail { get; set; }
        public Gender UserGender { get; set; }
        [Required]
        public int? UserAge { get; set; }
        [Required]
        [StringLength(20)]
        public string? UserPhoneNum { get; set; }
        [StringLength(50)]
        public string? UserAddress { get; set; }
        [Required]
        [StringLength(255)]
        public string? UserPassword { get; set; }
        public UserRole UserRole { get; set; } = UserRole.user;
        public ICollection<Orders>? Orders { get; set; }
        public ICollection<UserNotification>? UserNotifications { get; set; }
        public ICollection<Interest>? Interests { get; set; }

    }
}
