using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryOnline.Models
{
    public enum RequestStatus
    {
        Pending,
        Approved,
        Rejected
    }

    public class Orders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public Users User { get; set; }

        public int ArtId { get; set; }
        [ForeignKey("ArtId")]
        public ArtWork ArtWork { get; set; }

        public DateTime OrderDate { get; set; }

        public RequestStatus RequestStatus { get; set; }

    }
}
