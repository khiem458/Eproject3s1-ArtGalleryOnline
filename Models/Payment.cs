using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryOnline.Models
{
    public enum PaymentType
    {
        Cash,
        OnlinePay
    }
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public Users User { get; set; }

        public PaymentType PaymentType { get; set; }
        public int Amount { get; set; }
        public DateTime PaymentDate { get; set; }



    }
}
