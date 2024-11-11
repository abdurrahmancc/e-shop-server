using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using e_shop_server.DTOs;
using e_shop_server.Models;

namespace e_shop_server.Profiles
{
    public class ProductProfile : Profile
    {
        public  ProductProfile(){
            CreateMap<ProductModel, ProductReadDto>();
            CreateMap<ProductCreateDto, ProductModel>();

        }
    }
}