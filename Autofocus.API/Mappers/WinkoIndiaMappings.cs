using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofocus.Models;
using Autofocus.API.Dtos;
using Autofocus.Entity;

namespace Autofocus.API.Mappers
{
    public class WinkoIndiaMappings:Profile
    {
        public WinkoIndiaMappings()
        {
            CreateMap<MainCategory, MainCategoryDtos>().ReverseMap();
            CreateMap<MainCategory, MainCategoryInsertDtos>().ReverseMap();
            CreateMap<Subcategory, SubcategoryDtos>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserViewModelDtos>().ReverseMap();
            CreateMap<CountryRegistration, CountryRegistrationViewModel>().ReverseMap();
            CreateMap<StateRegistration, StateRegistrationViewModel>().ReverseMap();
            CreateMap<CityRegistration, CityRegistrationViewModelDtos>().ReverseMap();
            CreateMap<Grade, GradeViewModel>().ReverseMap();
            CreateMap<packingSize, packingSizeViewModel>().ReverseMap();
            CreateMap<packingType, packingTypeViewModel>().ReverseMap();
            CreateMap<slider, sliderViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModelDtos>().ReverseMap();
            CreateMap<ProductDetails, ProductDetailsViewModelDtos>().ReverseMap();
        }
    }
}
