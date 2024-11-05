using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_shop_server.Models
{
    public class Product
    {
        public Guid _id { get; set; }
        [Required(ErrorMessage = "Product Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Product price is required")]
        public int Price { get; set; }
        public int RegularPrice { get; set; }
        [Required(ErrorMessage = "Product quantity is required")]
        public int Quantity { get; set; }
        public int ReviewQuantity { get; set; }
        [Required(ErrorMessage = "Product category is required")]
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Review { get; set; }
        [Required(ErrorMessage = "Product image is required")]
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


public class SpecificationSection
{
    public Dictionary<string, List<object>> Details { get; set; }
}

}