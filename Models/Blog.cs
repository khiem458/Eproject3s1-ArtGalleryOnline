using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryOnline.Models
{
    public class Blog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlogId { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public Users User { get; set; }

        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Image { get; set; }
        [Required]
        public DateTime? Date { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Comment { get; set; }

    }
}
