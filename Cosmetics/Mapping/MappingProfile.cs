﻿using AutoMapper;
using Cosmetics.DTO.Brand;
using Cosmetics.DTO.Category;
using Cosmetics.DTO.Product;
using Cosmetics.DTO.User;
using Cosmetics.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cosmetics.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();  
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Brand, BrandDTO>().ReverseMap();
        }
    }
}
