using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using e_shop_server.data;
using e_shop_server.DTOs;
using e_shop_server.Interfaces;
using e_shop_server.Models;
using Microsoft.EntityFrameworkCore;

namespace e_shop_server.Services
{
  public class ProductService: IProductService
  {
    private static readonly List<ProductModel> _products = new List<ProductModel>();
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public ProductService(IMapper mapper, AppDbContext appDbContext)
    {
      _mapper = mapper;
      _appDbContext = appDbContext;
    }


public async Task<PaginatedResult<ProductReadDto>> GetAllProductsService(int pageNumber, int pageSize)
{
    // Validate pageNumber and pageSize
    if (pageNumber < 1) pageNumber = 1;
    if (pageSize < 1) pageSize = 10;

    // Get the total count of products
    var totalItems = await _appDbContext.Products.CountAsync();

    // Calculate total pages
    var totalPage = (int)Math.Ceiling((double)totalItems / pageSize);

    // Adjust pageNumber to stay within bounds
    pageNumber = Math.Max(1, Math.Min(pageNumber, totalPage));

    // Calculate the number of items to skip
    var skip = (pageNumber - 1) * pageSize;

    // Fetch paginated products
    var productList = await _appDbContext.Products.Skip(skip).Take(pageSize).ToListAsync();

    // Map to DTO
    var result = _mapper.Map<List<ProductReadDto>>(productList);

    // Build the paginated response
    var pager = new PaginatedResult<ProductReadDto>
    {
        Items = result,
        TotalItems = totalItems,
        PageNumber = pageNumber,
        PageSize = pageSize,
        StartPage = Math.Max(1, pageNumber - 2),
        EndPage = Math.Min(totalPage, pageNumber + 2)
    };

    return pager;
}



    public ProductReadDto? GetProductsByIdService(Guid Id)
    {
      var result = _products.FirstOrDefault(prod => prod._id == Id);

      return result == null ? null : _mapper.Map<ProductReadDto>(result);
    }



    public async Task<ProductReadDto> CreateProductService(ProductCreateDto productData)
    {
      var newProduct = _mapper.Map<ProductModel>(productData);

      newProduct._id = Guid.NewGuid();
      newProduct.UpdatedAt = DateTime.UtcNow;
      newProduct.CreatedAt = DateTime.UtcNow;
      newProduct.Date = DateTime.UtcNow;
      await _appDbContext.AddAsync(newProduct);
      await _appDbContext.SaveChangesAsync();

      return _mapper.Map<ProductReadDto>(newProduct);
    }


    public List<ProductReadDto>? GetProductsBySearchValueService(string searchData){
       var searchResultProducts = _products.Where(prod => prod.Name.Contains(searchData, StringComparison.OrdinalIgnoreCase)).ToList();

            if (searchResultProducts == null || !searchResultProducts.Any())
            {
                return null;
            }

           return  _mapper.Map<List<ProductReadDto>>(searchResultProducts);
    }



    public List<ProductReadDto>? GetProductsByIdsService(List<Guid> Ids){
      var foundProducts = _products.Where(prod => Ids.Contains(prod._id));
            if (foundProducts == null || !foundProducts.Any())
            {
                return null;
            }

            return _mapper.Map<List<ProductReadDto>>(foundProducts);
    }


    public List<ProductReadDto> GetProductsByPriceService(int minPrice, int maxPrice){
      var foundProduct = _products.Where(prod => prod.Price >= minPrice && prod.Price <= maxPrice).ToList();
            if (foundProduct == null || !foundProduct.Any())
            {
              return  null;

            }
           return _mapper.Map<List<ProductReadDto>>(foundProduct);
    }

  }
}