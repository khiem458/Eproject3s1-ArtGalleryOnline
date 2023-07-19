using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryOnline.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotifiId { get; set; }
        [Required]
        public string? Message { get; set; }
        [Required]
        public DateTime NotifiDate { get; set; }
    }
}
