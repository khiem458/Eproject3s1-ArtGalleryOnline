using Microsoft.Build.Framework;

namespace ArtGalleryOnline.Models
{
	public class SendmailDot
	{
		[Required]
		public string?Name { get; set; }
		[Required]
		public int phonenumber { get; set; }
		[Required]
		public string? Email { get; set; }
		[Required]
		public string? Description { get; set; }
	}
}
