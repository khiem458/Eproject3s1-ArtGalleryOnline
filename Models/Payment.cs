using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryOnline.Models
{
    public enum PaymentType
    {
        Cash = 1,
        OnlinePay = 2
    }
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }
        [Required]
        public string? PaymentMethod { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }

        ///lien ket voi oder
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Orders? Orders { get; set; }



    }
}
