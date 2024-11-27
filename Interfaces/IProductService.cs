using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using e_shop_server.DTOs;
using e_shop_server.Services;

namespace e_shop_server.Interfaces
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductReadDto>> GetAllProductsService(int currentPage, int pageSize);

        ProductReadDto? GetProductsByIdService(Guid Id);

        Task<ProductReadDto> CreateProductService(ProductCreateDto productData);

        List<ProductReadDto>? GetProductsBySearchValueService(string searchData);

        List<ProductReadDto>? GetProductsByIdsService(List<Guid> Ids);

        List<ProductReadDto> GetProductsByPriceService(int minPrice, int maxPrice);

    }
}