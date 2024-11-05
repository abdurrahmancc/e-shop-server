using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_shop_server.DTOs
{
    public class ProductRead
    {
        public Guid _id { get; set; }
        [Required(ErrorMessage = "Product Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Product price is required")]
        public int Price { get; set; }
        public int Quantity { get; set; }
        public List<string> Img { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }


    public class SpecificationSection
{
    public Dictionary<string, List<object>> Details { get; set; }
}
}