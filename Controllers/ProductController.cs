using System;
using System.Collections.Generic;
using System.Diagnostics;
using e_shop_server.DTOs;
using e_shop_server.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_shop_server.Controllers
{
    [ApiController]
    [Route("v1/api/products")]
    public class ProductController : ControllerBase
    {
        private static List<ProductModel> products = new List<ProductModel>();

        [HttpGet]
        public ActionResult GetProducts()
        {
            var productList = products.Select(prod=>new ProductRead{
                _id = prod._id,
                Name = prod.Name,
                Price = prod.Price,
                Quantity = prod.Quantity,
                Img = prod.Img,
                UpdatedAt = prod.UpdatedAt
            }).ToList();

            return Ok(ApiResponse<List<ProductRead>>.SuccessResponse(productList, 201, "successful"));
        }

        [HttpGet("{Id:Guid}")]
        public IActionResult GetProductById(Guid Id)
        {

            var foundData = products.FirstOrDefault(prod => prod._id == Id);

            if (foundData == null)
            {
                return NotFound();
            }

            var data = new ProductRead
            {
                _id = foundData._id,
                Name = foundData.Name,
                Price = foundData.Price,
                Quantity = foundData.Quantity,
                Img = foundData.Img,
                UpdatedAt = foundData.UpdatedAt,
            };

            return Ok(data);

        }

        [HttpPost]
        public IActionResult CreateProducts([FromBody] ProductCreate productData)
        {

            var newProduct = new ProductModel
            {
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

        var ProductCreateDto = new ProductCreate
            {
                _id = newProduct._id,
                Name = newProduct.Name,
                Price = newProduct.Price,
                RegularPrice = newProduct.RegularPrice,
                Quantity = newProduct.Quantity,
                ReviewQuantity = newProduct.ReviewQuantity,
                Category = newProduct.Category,
                SubCategory = newProduct.SubCategory,
                Date = newProduct.Date,
                Status = newProduct.Status,
                Img = newProduct.Img,
                Description = newProduct.Description,
                Badge = newProduct.Badge,
                Model = newProduct.Model,
                SKU = newProduct.SKU,
                ShortFeatures = newProduct.ShortFeatures,
                Specifications = productData.Specifications,
                UpdatedAt = newProduct.UpdatedAt,
                CreatedAt = newProduct.CreatedAt
            };

            products.Add(newProduct);

            return Created($"v1/api/products/{ProductCreateDto._id}", ProductCreateDto);
        }
    }
}
