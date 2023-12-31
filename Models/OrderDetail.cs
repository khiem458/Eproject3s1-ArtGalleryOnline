﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryOnline.Models
{
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailId { get; set; }
        //lien ket voi oder
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Orders? Orders { get; set; }
        public int ArtId { get; set; }
        [ForeignKey("ArtId")]
        public ArtWork? ArtWork { get; set; }
        public decimal Price { get; set; }       
        public int Quantity { get; set; }
        // liet với artwork
       
    }
}
