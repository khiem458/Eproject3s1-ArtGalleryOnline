using System.ComponentModel.DataAnnotations;

namespace ArtGalleryOnline.Models
{
    public class AuthorizationRequest
    {
        [Key]
        public int RequestId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public bool IsApproved { get; set; }

        public DateTime RequestDate { get; set; }
    }
}
