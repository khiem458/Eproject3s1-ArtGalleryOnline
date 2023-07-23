namespace ArtGalleryOnline.Models
{
    public class Cart
    {


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


    }
    public class CartItem
    {
        public int ArtId { get; set; }
        public string ArtName { get; set; }
        public decimal ArtPrice { get; set; }
        public int Quantity { get; set; }
    }
}
