using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryOnline.Models
{
    public enum Type
    {
        Scriptures,
        Painting,
        Model
    }
    public class ArtWork
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArtId { get; set; }
        [Required]
        public string ArtName { get; set; }
        [Required]
        public string Detail { get; set; }
        [Required]
        public Decimal? Price { get; set; }
        [Required]
        public int? Quantity { get; set; }
        [Required]
        public Type Type { get; set; }
        [Required]
        public bool AdminApproval { get; set; }

    }
}
