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
        [StringLength(50)]
        public string? ArtName { get; set; }
        [Required]
        [StringLength(50)]
        public string? ArtDescription { get; set; }
        [Required]
        [StringLength(50)]
        public string? ArtImage { get; set; }
        [Required]
        public decimal ArtPrice { get; set; }
        //lien ket voi User
        public int AuthId { get; set; }
        [ForeignKey("AuthId")]
        public AuthorArtWork? AuthorArtWork { get; set; }


        public ICollection<Interest>? Interests { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
