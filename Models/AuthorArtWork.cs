using ArtGalleryOnline.ModelsView;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryOnline.Models
{
    public class AuthorArtWork 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthId { get; set; }
        [Required]
        public string? Artist { get; set; }
        [Required]
        public string? AuthDesciption { get; set; }
        public ICollection<ArtWork>? ArtWorks { get; set; }
        public ICollection<Blog>? Blogs { get; set; }
    }
}
