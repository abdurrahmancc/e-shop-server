using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using e_shop_server.DTOs;
using e_shop_server.Models;

namespace e_shop_server.Services
{
    public class ProductService
    {
          private static readonly List<ProductModel> products = new List<ProductModel>();
          private readonly IMapper _mapper;

          public ProductService(IMapper mapper){
            _mapper = mapper;
          }


          public ItemsListWithPagination<List<ProductReadDto>> GetAllProducts(int currentPage, int pageSize){
            var totalItems = products.Count;
            var totalPage = (int)Math.Ceiling((double)totalItems / pageSize);
            currentPage = Math.Max(1, Math.Min(currentPage, totalPage));

            var skip = (currentPage - 1) * pageSize;

            var productList = products.Skip(skip).Take(pageSize).Select(prod => new ProductReadDto
            {
                _id = prod._id,
                Name = prod.Name,
                Price = prod.Price,
                Quantity = prod.Quantity,
                Img = prod.Img,
                UpdatedAt = prod.UpdatedAt
            }).ToList();

            var pager = new PagerModel
            {
                TotalItems = totalItems,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalPage = totalPage,
                StartPage = Math.Max(1, currentPage - 2),
                EndPage = Math.Min(totalPage, currentPage + 2)
            };

            var responseData = new ItemsListWithPagination<List<ProductReadDto>>
            {
                ItemsList = productList,
                Pager = pager

            };

            return responseData;
          }
          
    }
}