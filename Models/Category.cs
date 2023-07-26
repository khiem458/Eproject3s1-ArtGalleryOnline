using ArtGalleryOnline.ModelsView;
using System.ComponentModel.DataAnnotations;

namespace ArtGalleryOnline.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string? CategoryName { get; set; }
        [Required]
        public string? CategoryDescription { get; set; }
        public ICollection<ArtWork>? ArtWorks { get; set; }
    }
}
