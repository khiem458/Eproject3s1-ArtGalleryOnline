
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryOnline.Models
{
    public class Interest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InterestId { get; set; }

        //lien ket voi User
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public Users? User { get; set; }

        //lieen ket voi ArtWork
        public int ArtId { get; set; }

        [ForeignKey("ArtId")]
        public ArtWork? ArtWork { get; set; }

    }
}
