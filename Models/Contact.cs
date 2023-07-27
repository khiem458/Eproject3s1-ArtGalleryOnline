using System.ComponentModel.DataAnnotations;

namespace ArtGalleryOnline.Models
{
    public class Contact
    {
        [Key]
        public string? ContactName { get; set; }
        [Required]
        public string? ContactTitle { get; set; }
        [Required]
        public string? ContactEmail { get; set; }
        [Required]
        public string? ContactPhone { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}
