using AutoMapper;
using Sophos.API.Models;
using Sophos.API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sophos.API.ApiMapper
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Client, ClientDTO>().ReverseMap();
            CreateMap<Sale, SaleDTO>().ReverseMap();
        }
    }
}
