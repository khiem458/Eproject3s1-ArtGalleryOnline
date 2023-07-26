using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryOnline.Models
{
	public class CommentBlog
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentBlogId { get; set; }
        [Required]
        public int BlogId { get; set; }
        [Required]
		public string? Name { get; set; }
		public string? Email { get; set; }
		public DateTime DatePosted { get; set; }
		public string? Message { get; set; }

        [ForeignKey("BlogId")]
        public Blog? Blog { get; set; }
    }
}
