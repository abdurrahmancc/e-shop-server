using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using e_shop_server.DTOs;
using e_shop_server.Services;
using Microsoft.AspNetCore.Mvc;

namespace e_shop_server.Interfaces
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductReadDto>> GetAllProducts(  int currentPage, int pageSize, string search, string sortOrder);

        Task<ProductReadDto?> GetProductsByIdService(Guid Id);

        Task<ProductReadDto> CreateProductService(ProductCreateDto productData);

        Task<List<ProductReadDto>?> GetProductsBySearchValueService(string searchData);

        Task<List<ProductReadDto>?> GetProductsByIdsService(List<Guid> Ids);

        Task<List<ProductReadDto>> GetProductsByPriceService(int minPrice, int maxPrice);

    }
}