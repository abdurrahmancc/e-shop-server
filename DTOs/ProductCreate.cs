using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using e_shop_server.Models;

namespace e_shop_server.DTOs
{
    public class ProductCreate
    {
        public Guid _id { get; set; }
        [Required( ErrorMessage = "Product name is required")]
        [StringLength(100, MinimumLength =2, ErrorMessage ="Product name is must be at least 2 character long")]
        public string Name { get; set; }
        public int Price { get; set; }
        public int RegularPrice { get; set; }
        public int Quantity { get; set; }
        public int ReviewQuantity { get; set; }

        public string Category { get; set; }
        public string SubCategory { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Review { get; set; }
        public List<string> Img { get; set; }
        public string Description { get; set; }
        public string Badge { get; set; }
        public string Model { get; set; }
        public string SKU { get; set; }
        public List<string> ShortFeatures { get; set; }
        public List<SpecificationSection> Specifications { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
  
}