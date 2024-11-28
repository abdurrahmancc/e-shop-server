using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using e_shop_server.data;
using e_shop_server.DTOs;
using e_shop_server.Interfaces;
using e_shop_server.Models;
using e_shop_server.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace e_shop_server.Services
{
  public class ProductService : IProductService
  {
    private static readonly List<ProductModel> _products = new List<ProductModel>();
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public ProductService(IMapper mapper, AppDbContext appDbContext)
    {
      _mapper = mapper;
      _appDbContext = appDbContext;
    }


    public async Task<PaginatedResult<ProductReadDto>> GetAllProducts(int pageNumber, int pageSize, string search, string sortOrder)
    {

      IQueryable<ProductModel> query = _appDbContext.Products;

      if (!string.IsNullOrWhiteSpace(search))
      {
        var formattedSearch = $"%{search.Trim().ToLower()}%";
        query = query.Where(Prod => EF.Functions.ILike(Prod.Name, formattedSearch) || EF.Functions.ILike(Prod.Description, formattedSearch));
      }

      if (!string.IsNullOrWhiteSpace(sortOrder))
      {
        var formattedSortOrder = sortOrder.Trim().ToLowerInvariant();
        if (Enum.TryParse<SortOrder>(formattedSortOrder, true, out var formattedSortOrderParse))
        {
          query = formattedSortOrderParse switch
          {
            SortOrder.name_asc => query.OrderBy(P => P.Name),
            SortOrder.name_desc => query.OrderByDescending(P => P.Name),
            SortOrder.price_asc => query.OrderBy(P => P.Price),
            SortOrder.price_desc => query.OrderByDescending(P => P.Price),
            SortOrder.create_asc => query.OrderBy(P => P.CreatedAt),
            SortOrder.create_desc => query.OrderByDescending(P => P.CreatedAt),
            _ => query.OrderBy(P => P.Name)
          };

        }
      }


      var totalItems = await query.CountAsync();
      var totalPage = (int)Math.Ceiling((double)totalItems / pageSize);
      pageNumber = Math.Max(1, Math.Min(pageNumber, totalPage));

      var skip = (pageNumber - 1) * pageSize;

      var productList = query.Skip(skip).Take(pageSize).ToList();
      var result = _mapper.Map<List<ProductReadDto>>(productList);
      return new PaginatedResult<ProductReadDto>
      {
        Items = result,
        TotalItems = totalItems,
        PageNumber = pageNumber,
        PageSize = pageSize,
        StartPage = Math.Max(1, pageNumber - 2),
        EndPage = Math.Min(totalPage, pageNumber + 2)
      };
    }


    public async Task<ProductReadDto?> GetProductsByIdService(Guid Id)
    {
      var result = await _appDbContext.Products.FindAsync(Id);

      return result == null ? null : _mapper.Map<ProductReadDto>(result);
    }



    public async Task<ProductReadDto> CreateProductService(ProductCreateDto productData)
    {
      var newProduct = _mapper.Map<ProductModel>(productData);

      newProduct._id = Guid.NewGuid();
      newProduct.UpdatedAt = DateTime.UtcNow;
      newProduct.CreatedAt = DateTime.UtcNow;
      newProduct.Date = DateTime.UtcNow;
      await _appDbContext.Products.AddAsync(newProduct);
      await _appDbContext.SaveChangesAsync();

      return _mapper.Map<ProductReadDto>(newProduct);
    }


    public async Task<List<ProductReadDto>?> GetProductsBySearchValueService(string searchData)
    {
      var searchResultProducts = await _appDbContext.Products.Where(prod => EF.Functions.ILike(prod.Name, $"%{searchData}%")).ToListAsync();

      if (searchResultProducts == null || !searchResultProducts.Any())
      {
        return null;
      }

      return _mapper.Map<List<ProductReadDto>>(searchResultProducts);
    }



    public async Task<List<ProductReadDto>?> GetProductsByIdsService(List<Guid> Ids)
    {
      var foundProducts = await _appDbContext.Products.Where(prod => Ids.Contains(prod._id)).ToListAsync();
      if (foundProducts == null || !foundProducts.Any())
      {
        return null;
      }

      return _mapper.Map<List<ProductReadDto>>(foundProducts);
    }


    public async Task<List<ProductReadDto>> GetProductsByPriceService(int minPrice, int maxPrice)
    {
      var foundProduct = await _appDbContext.Products.Where(prod => prod.Price >= minPrice && prod.Price <= maxPrice).ToListAsync();
      if (foundProduct == null || !foundProduct.Any())
      {
        return null;

      }
      return _mapper.Map<List<ProductReadDto>>(foundProduct);
    }

  }
}