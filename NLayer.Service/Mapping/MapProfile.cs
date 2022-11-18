﻿using AutoMapper;
using NLayer.Core;
using NLayer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();//.ReverseMap() ile tersi işlem de geçerlidir
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<ProductFeature, ProductFeature>().ReverseMap();
            CreateMap<ProductFeature, Product>();//tersine burada ihtiyaç yok

        }
    }
}
