namespace ArtGalleryOnline.Models
{
    public class Cart
    {
        

        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public void AddItem(ArtWork artWork, int quantity)
        {
            CartItem? item = Items.Where(p=>p.ArtWork.ArtId== artWork.ArtId).FirstOrDefault();
            if(item == null)
            {
                Items.Add(new CartItem
                {
                    ArtWork = artWork,
                    Quantity = quantity
                });
            }
            else
            {
                item.Quantity += quantity;
            }
        }


    }
    public class CartItem
    {
        public int CartId { get; set; }
        public ArtWork ArtWork { get; set; }= new ArtWork();
        public int Quantity { get; set; }
    }
}
