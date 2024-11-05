using System;
using System.Collections.Generic;
using e_shop_server.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_shop_server.Controllers
{
    [ApiController]
    [Route("v1/api/products")]
    public class ProductController : ControllerBase
    {
        private static List<Product> products = new List<Product>();

        [HttpGet]
        public ActionResult GetProducts()
        {
            return Ok(products);
        }

        [HttpPost]
        public ActionResult CreateProducts([FromBody] Product productData)
        {
            var newProduct = new Product{
                _id = Guid.NewGuid(),
                Name = productData.Name,
                Price = productData.Price,
                RegularPrice = productData.RegularPrice,
                Quantity = productData.Quantity,
                ReviewQuantity = productData.ReviewQuantity,
                Category = productData.Category,
                SubCategory = productData.SubCategory,
                Date = DateTime.UtcNow,
                Status = productData.Status,
                Img = productData.Img,
                Description = productData.Description,
                Badge = productData.Badge,
                Model = productData.Model,
                SKU = productData.SKU,
                ShortFeatures = productData.ShortFeatures,
                Specifications = productData.Specifications,
                UpdatedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };
            products.Add(newProduct);

            return Ok(newProduct);
        }
    }
}
