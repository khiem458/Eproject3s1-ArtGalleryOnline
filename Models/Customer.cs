using System.ComponentModel.DataAnnotations;

namespace ArtGalleryOnline.Models
{
    public class Customer
    {
       
            [Required(ErrorMessage = "First Name is required")]
            public string? CutomerName { get; set; }

            [Required(ErrorMessage = "Last Name is required")]
            public string? LastName { get; set; }

            [Required(ErrorMessage = "Email is required")]
            [EmailAddress(ErrorMessage = "Invalid Email Address")]
            public string? Email { get; set; }

            [Required(ErrorMessage = "Mobile No is required")]
            public string? MobileNo { get; set; }

            [Required(ErrorMessage = "Address Line 1 is required")]
            public string? AddressLine1 { get; set; }

            public string? ShippingAddress { get; set; }

            [Required(ErrorMessage = "Country is required")]
            public string? Country { get; set; }

            [Required(ErrorMessage = "City is required")]
            public string? City { get; set; }

            [Required(ErrorMessage = "State is required")]
            public string? State { get; set; }

            [Required(ErrorMessage = "ZIP Code is required")]
            public string? ZIPCode { get; set; }
            public int Payment { get;set; }


    }
}
