using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryOnline.Models
{
    // Mô tả trạng thái yêu cầu (request status) dưới dạng enum
    public enum RequestStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2
    }

    public class Orders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        // Thuộc tính OrderDate đại diện cho ngày đặt hàng, được yêu cầu và không thể null

        public DateTime OrderDate { get; set; }

        // Thuộc tính RequestStatus đại diện cho trạng thái yêu cầu, có kiểu enum RequestStatus
        // Theo mô tả của bạn, trạng thái yêu cầu có thể là "Pending", "Approved", hoặc "Rejected"
        public RequestStatus RequestStatus { get; set; }

        public decimal TotalAmount { get; set; }
        // Tên người nhận hàng
        public string? RecipientName { get; set; }

        // Địa chỉ giao hàng
        public string? ShippingAddress { get; set; }

        // Số điện thoại liên hệ người nhận hàng
        public string? RecipientPhone { get; set; }

        // Địa chỉ email liên hệ người nhận hàng
        public string? RecipientEmail { get; set; }

        // Thuộc tính UserId đại diện cho khóa ngoại liên kết với bảng "Users"
        public int UserId { get; set; }

        // Thuộc tính User là Navigation property để truy cập thông tin người dùng (từ bảng "Users")
        [ForeignKey("UserId")]
        public Users? User { get; set; }


        public ICollection<OrderDetail>? OrderDetails { get; set; }
        public ICollection<Payment>? Payments { get; set; }

    }
}
