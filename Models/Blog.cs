using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryOnline.Models
{
    public class Blog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlogId { get; set; }
        [Required]
        [StringLength(50)]
        public string? Title { get; set; }
        
        [StringLength(50)]
        public string? Image { get; set; }
        [Required]
        public DateTime? Date { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        [StringLength(500)]
        public string? Comment { get; set; }

        // lien ket voi Author
        public int AuthId { get; set; }

        [ForeignKey("AuthId")]
        public AuthorArtWork? AuthorArtWork { get; set; }

    }
}
