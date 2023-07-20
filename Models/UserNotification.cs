using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryOnline.Models
{
    public class UserNotification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UnotifiId { get; set; }

        //lien ket voi Notification
        public int NotifiId { get; set; }
        [ForeignKey("NotifiId")]
        public Notification? Notification { get; set; }
        
        //lien ket voi User
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public Users? Users { get; set; }
    }
}
