using ArtGalleryOnline.ModelsView;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryOnline.Models
{
    
   
    public class ArtWork
    {
         
       
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArtId { get; set; }
        [Required]
        [StringLength(50)]
        public string? ArtName { get; set; }
        [Required]
     
        public string? ArtDescription { get; set; }
        
        public string? ArtImage { get; set; }
        [Required(ErrorMessage = "ArtPrice is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "ArtPrice must be a positive number.")]
        public decimal ArtPrice { get; set; }
        // Số lượng tồn kho
        public int StockQuantity { get; set; }
        //lien ket voi Author
        public int AuthId { get; set; }
        [ForeignKey("AuthId")]
        public AuthorArtWork? AuthorArtWork { get; set; }

        //lien ket voi Category
        public int CategoryId { get; set; }
        [ForeignKey(" CategoryId")]
        public Category? Category { get; set; }

        public ICollection<Interest>? Interests { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
