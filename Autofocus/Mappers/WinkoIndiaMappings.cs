using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofocus.Models;
using Autofocus.Models.Dtos;
using Autofocus.Entity;

namespace Autofocus.Mappers
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
            CreateMap<ApplicationUser, LoginWhiteMobilenoandPasswordDtos>().ReverseMap();
            CreateMap<ApplicationUser, BasicInformationDtos>().ReverseMap();
            CreateMap<ApplicationUser, CertificationInformationDtos>().ReverseMap();
            CreateMap<ApplicationUser, DocumentationDtos>().ReverseMap();
            CreateMap<Subcategory, SubcategoryDtos>().ReverseMap();
            CreateMap<Subcategory, SubcategoryUpsertDtos>().ReverseMap();
            CreateMap<ProductMaster, ProductMasterDtos>().ReverseMap();
            CreateMap<ProductMaster, ProductMasterUpsertDtos>().ReverseMap();
            CreateMap<productSize, productSizeViewModelDtos>().ReverseMap();
            CreateMap<Product, ProductbyIdViewModelDtos>().ReverseMap();
            CreateMap<ApplicationUser, UserInformationViewModelDtos>().ReverseMap();
            //CreateMap<ProductDetails, ProductDetailsbyidViewModelDtos>().ReverseMap();

        }
    }
}
