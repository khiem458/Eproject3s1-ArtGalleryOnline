using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryOnline.Models
{
    public enum Gender
    {
        Male,
        Female
    }
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? FullName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public int? Age { get; set; }
        [Required]
        public string? PhoneNum { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? UserEmail { get; set; }

    }
}
