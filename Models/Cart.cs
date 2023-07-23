namespace ArtGalleryOnline.Models
{
    public class Cart
    {
<<<<<<< HEAD


        private List<CartItem> items = new List<CartItem>();

        public IReadOnlyCollection<CartItem> Items => items.AsReadOnly();

        public void AddItem(ArtWork artWork, int quantity)
        {
            var existingItem = items.FirstOrDefault(item => item.ArtId == artWork.ArtId);

            if (existingItem == null)
            {
                var newItem = new CartItem
                {
                    ArtId = artWork.ArtId,
                    ArtName = artWork.ArtName,
                    ArtPrice = artWork.ArtPrice,
                    Quantity = quantity
                };
                items.Add(newItem);
            }
            else
            {
                existingItem.Quantity += quantity;
            }
        }

        // Thêm các phương thức xóa mục, cập nhật số lượng, v.v. nếu cần thiết

=======
        

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

>>>>>>> 37403b2cf1321335730aaed83669456188fe7316

    }
    public class CartItem
    {
<<<<<<< HEAD
        public int ArtId { get; set; }
        public string ArtName { get; set; }
        public decimal ArtPrice { get; set; }
=======
        public int CartId { get; set; }
        public ArtWork ArtWork { get; set; }= new ArtWork();
>>>>>>> 37403b2cf1321335730aaed83669456188fe7316
        public int Quantity { get; set; }
    }
}
