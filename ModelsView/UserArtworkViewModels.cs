using ArtGalleryOnline.Models;

namespace ArtGalleryOnline.ModelsView
{
    public class UserArtworkViewModels
    {
        public IEnumerable<Users> Users { get; set; }
        public IEnumerable<ArtWork> ArtWorks { get; set; }
    }
}
