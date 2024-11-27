using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Security.Cryptography;
using System.Xml.Linq;
using e_shop_server.DTOs;
using e_shop_server.Interfaces;
using e_shop_server.Models;
using e_shop_server.Services;
using e_shop_server.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace e_shop_server.Controllers.Products
{
    [ApiController]
    [Route("v1/api/products")]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService){
            _productService = productService;
        }

        //GET: v1/api/products/getProducts?currentPage=2&pageSize=5 get data with query params
        [Authorize(Roles = "Admin,User,Moderator")]
        [HttpGet]
        [Route("getProducts")]
        public async Task<ActionResult> GetAllProducts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 3)
        {
            var responseData = await _productService.GetAllProductsService(pageNumber, pageSize);

            if(responseData.Items == null || !responseData.Items.Any())
            {
                return NotFound(ApiResponse<Object>.ErrorResponse(new List<string> { $"dose not exit products" }, 404, "Validation failed"));
            }

            return Ok(ApiResponse<PaginatedResult<ProductReadDto>>.SuccessResponse(responseData, 200, "Get successful"));
        }




        //GET:  v1/api/products/GetProductsById/id--- get product by id
        [HttpGet("getProductsById/{Id:Guid}")]
        public IActionResult GetProductsById(Guid Id)
        {

            var result = _productService.GetProductsByIdService(Id);

            if (result == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { "product with this id does not exist" }, 404, "Validation failed"));
            }

            return Ok(ApiResponse<ProductReadDto>.SuccessResponse(result, 200, "Get successful"));

        }
        
        
        
        //POST: http://localhost:5121/v1/api/products create a product
        [HttpPost]
        public async Task<IActionResult> CreateProducts([FromBody] ProductCreateDto productData)
        {

            var result = await _productService.CreateProductService(productData);
            return Created(nameof(GetProductsById), ApiResponse<ProductReadDto>.SuccessResponse(result, 201, "Product create successful"));
        }


        //GET: /v1/api/products/getProductSearchValue/?searchData=app get products by search value
        [HttpGet]
        [Route("getProductSearchValue")]
        public IActionResult GetProductsBySearchValue(string searchData)
        {
            var result = _productService.GetProductsBySearchValueService(searchData);
            if (result == null || !result.Any())
            {
                return NotFound(ApiResponse<Object>.ErrorResponse(new List<string> { $"Product with this {searchData} dose not exit" }, 404, "Validation failed"));
            }

            return Ok(ApiResponse<List<ProductReadDto>>.SuccessResponse(result, 200, "Success"));
        }


        //POST: v1/api/products/getProductsByIds get products by multiple ids
        [HttpPost]
        [Route("getProductsByIds")]
        public IActionResult GetProductsByIds(List<Guid> Ids)
        {

            var foundProducts = _productService.GetProductsByIdsService(Ids);
            if (foundProducts == null || !foundProducts.Any())
            {
                return NotFound(ApiResponse<Object>.ErrorResponse(new List<string> { $"Product with these ids dose not exit" }, 404, "Validation failed"));
            }

            return Ok(ApiResponse<List<ProductReadDto>>.SuccessResponse(foundProducts, 200, "Success"));
        }


        //GET: http://localhost:5121/v1/api/products/getProductByPrice?minPrice=10&maxPrice=100 get product by min and max prices
        [HttpGet]
        [Route("getProductByPrice")]
        public IActionResult GetProductsByPrice(int minPrice = 5, int maxPrice = 1000)
        {
            var foundProduct = _productService.GetProductsByPriceService(minPrice, maxPrice);
            if (foundProduct == null || !foundProduct.Any())
            {
                return NotFound(ApiResponse<Object>.ErrorResponse(new List<string> { $"Product with these prices dose not exit" }, 404, "Validation failed"));

            }

            return Ok(ApiResponse<List<ProductReadDto>>.SuccessResponse(foundProduct, 200, "successful"));
        }
    }
}
