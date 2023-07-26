using ArtGalleryOnline.Models;
using System.ComponentModel.DataAnnotations;

namespace ArtGalleryOnline.ModelsView
{
    public class OrderViewModel
    {
        // Tên người nhận hàng
        [Required]  
        public string? RecipientName { get; set; }

        // Địa chỉ giao hàng
        [Required]
        public string? ShippingAddress { get; set; }

        // Số điện thoại liên hệ người nhận hàng
        [Required]
        public string? RecipientPhone { get; set; }

        // Địa chỉ email liên hệ người nhận hàng
        public string? RecipientEmail { get; set; }
        public int TypePayment { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
