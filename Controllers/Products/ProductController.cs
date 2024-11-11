using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Security.Cryptography;
using System.Xml.Linq;
using e_shop_server.DTOs;
using e_shop_server.Models;
using e_shop_server.Services;
using e_shop_server.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace e_shop_server.Controllers.Products
{
    [ApiController]
    [Route("v1/api/products")]
    public class ProductController : ControllerBase
    {
        private static List<ProductModel> products = new List<ProductModel>();
        private ProductService _productService;

        public ProductController(ProductService productService){
            _productService = productService;
        }

        //GET: v1/api/products/getProducts?currentPage=2&pageSize=5 get data with query params
        [HttpGet]
        [Route("getProducts")]
        public ActionResult GetAllProducts(int currentPage = 1, int pageSize = 3)
        {
            var responseData = _productService.GetAllProducts(currentPage, pageSize);

            if(responseData.ItemsList == null || !responseData.ItemsList.Any())
            {
                return NotFound(ApiResponse<Object>.ErrorResponse(new List<string> { $"dose not exit products" }, 404, "Validation failed"));
            }

            return Ok(ApiResponse<ItemsListWithPagination<List<ProductReadDto>>>.SuccessResponse(responseData, 200, "Get successful"));
        }




        //GET:  v1/api/products/GetProductsById/id--- get product by id
        [HttpGet("getProductsById/{Id:Guid}")]
        public IActionResult GetProductsById(Guid Id)
        {

            var foundData = products.FirstOrDefault(prod => prod._id == Id);

            if (foundData == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { "product with this id does not exist" }, 404, "Validation failed"));
            }

            var data = new ProductReadDto
            {
                _id = foundData._id,
                Name = foundData.Name,
                Price = foundData.Price,
                Quantity = foundData.Quantity,
                Img = foundData.Img,
                UpdatedAt = foundData.UpdatedAt,
            };

            return Ok(ApiResponse<ProductReadDto>.SuccessResponse(data, 200, "Get successful"));

        }
        
        
        
        //POST: http://localhost:5121/v1/api/products create a product
        [HttpPost]
        public IActionResult CreateProducts([FromBody] ProductCreateDto productData)
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

            var ProductCreateDto = new ProductCreateDto
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

            return Created($"v1/api/products/{ProductCreateDto._id}", ApiResponse<ProductCreateDto>.SuccessResponse(ProductCreateDto, 201, "Product create successful"));
        }


        //GET: /v1/api/products/getProductSearchValue/?searchData=app get products by search value
        [HttpGet]
        [Route("getProductSearchValue")]
        public IActionResult GetProductsBySearchValue(string searchData)
        {
            var searchResultProducts = products.Where(prod => prod.Name.Contains(searchData, StringComparison.OrdinalIgnoreCase));
            if (searchResultProducts == null || !searchResultProducts.Any())
            {
                return NotFound(ApiResponse<Object>.ErrorResponse(new List<string> { $"Product with this {searchData} dose not exit" }, 404, "Validation failed"));
            }

            var result = searchResultProducts.Select(res => new ProductReadDto
            {
                _id = res._id,
                Name = res.Name,
                Price = res.Price,
                Quantity = res.Quantity,
                Img = res.Img,
                UpdatedAt = res.UpdatedAt,
            }).ToList();

            return Ok(ApiResponse<List<ProductReadDto>>.SuccessResponse(result, 200, "Success"));
        }


        //POST: v1/api/products/getProductsByIds get products by multiple ids
        [HttpPost]
        [Route("getProductsByIds")]
        public IActionResult GetProductsByIds(List<Guid> Ids)
        {

            var foundProducts = products.Where(prod => Ids.Contains(prod._id));
            if (foundProducts == null || !foundProducts.Any())
            {
                return NotFound(ApiResponse<Object>.ErrorResponse(new List<string> { $"Product with these ids dose not exit" }, 404, "Validation failed"));
            }

            var result = foundProducts.Select(res => new ProductReadDto
            {
                _id = res._id,
                Name = res.Name,
                Price = res.Price,
                Quantity = res.Quantity,
                Img = res.Img,
                UpdatedAt = res.UpdatedAt,
            }).ToList();

            return Ok(ApiResponse<List<ProductReadDto>>.SuccessResponse(result, 200, "Success"));
        }


        //GET: http://localhost:5121/v1/api/products/getProductByPrice?minPrice=10&maxPrice=100 get product by min and max prices
        [HttpGet]
        [Route("getProductByPrice")]
        public IActionResult GetProductsByPrice(int minPrice = 5, int maxPrice = 1000)
        {
            var foundProduct = products.Where(prod => prod.Price >= minPrice && prod.Price <= maxPrice);

            if (foundProduct == null || !foundProduct.Any())
            {
                return NotFound(ApiResponse<Object>.ErrorResponse(new List<string> { $"Product with these prices dose not exit" }, 404, "Validation failed"));

            }
            var result = foundProduct.Select(prod => new ProductReadDto
            {
                _id = prod._id,
                Name = prod.Name,
                Price = prod.Price,
                Quantity = prod.Quantity,
                Img = prod.Img,
                UpdatedAt = prod.UpdatedAt,
            }).ToList();

            return Ok(ApiResponse<List<ProductReadDto>>.SuccessResponse(result, 200, "successful"));
        }
    }
}
