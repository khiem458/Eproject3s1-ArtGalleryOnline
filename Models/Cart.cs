namespace ArtGalleryOnline.Models
{
    public class Cart
    {
       public List<CartItem> CartItems { get; set; }=new List<CartItem>();

       

        public void AddItem(ArtWork artWork, int quantity)
        {
           CartItem? item = CartItems.Where(p=>p.ArtWork.ArtId == artWork.ArtId).FirstOrDefault();

            if (item == null)
            {
                CartItems.Add(new CartItem
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
        public void RemoveItem(ArtWork artWork)=>CartItems.RemoveAll(l=>l.ArtWork.ArtId == artWork.ArtId);
        public decimal ComputeTotalValues() => CartItems.Sum(e => e.ArtWork.ArtPrice * e.Quantity);
        public void Clear()=>CartItems.Clear();

    }
    public class CartItem
    {
        public int CartItemId { get; set; }
        public ArtWork ArtWork { get; set; } = new();
        public int Quantity { get; set; }
    }
}
