using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_shop_server.DTOs
{
    public class ProductReadDto
    {
        public Guid _id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? FullName { get; set; }
        [Required]
        public int Price { get; set; }
        public int Quantity { get; set; }
        public List<string> Img { get; set; }
        public DateTime UpdatedAt { get; set; }

    }


}