using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    //Auto mapper used for, sob somoy jate Dto bebohar kore Controller e data return kora na lage sei jonne.(Need Automapper as a service in startup.cs file)
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
               .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))    //ProductToReturnDto hosse Product er ViewModel or Dto!
               .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
               .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());        //picture url will find ProductUrlResolver Class and this class's picture will find from appsettings.development.json
                 
        }
    }
}