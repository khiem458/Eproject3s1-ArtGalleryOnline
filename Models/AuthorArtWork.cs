using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryOnline.Models
{
    public class AuthorArtWork
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthId { get; set; }

        public int ArtId { get; set; }

        [ForeignKey("ArtId")] // Khóa phụ liên kết với khóa chính của ArtWork
        public ArtWork ArtWork { get; set; }

        [Required]
        public string Artist { get; set; }
        [Required]
        public string AuthDesciption { get; set; }
    }
}
