using AutoMapper;
using SampleWebApiAspNetCore.Dtos;
using SampleWebApiAspNetCore.Entities;
using Synzeal_API.Dtos;
using Synzeal_API.Entities;

namespace SampleWebApiAspNetCore.MappingProfiles
{
    public class FoodMappings : Profile
    {
        public FoodMappings()
        {
            CreateMap<FoodEntity, FoodDto>().ReverseMap();
            CreateMap<FoodEntity, FoodUpdateDto>().ReverseMap();
            CreateMap<FoodEntity, FoodCreateDto>().ReverseMap();


            CreateMap<SZ_CompanyList, SZ_CompanyListDto>().ReverseMap();
            CreateMap<SZ_CompanyListDto, SZ_CompanyList>().ReverseMap();

            CreateMap<SZ_QuotationDetail, SZ_QuotationDetailDto>().ReverseMap();
            CreateMap<SZ_QuotationDetailDto, SZ_QuotationDetail>().ReverseMap();

            //CreateMap<SZ_QuoteDetailForm, SZ_QuoteDetailFormDto>().ReverseMap();
            //CreateMap<SZ_QuoteDetailFormDto, SZ_QuoteDetailForm>().ReverseMap();

            CreateMap<SZ_Quotation, SZ_QuotationDto>().ReverseMap();
            CreateMap<SZ_QuotationDto, SZ_Quotation>().ReverseMap();

            //CreateMap<SZ_ClubQuote, SZ_ClubQuoteDto>().ReverseMap();
            //CreateMap<SZ_ClubQuoteDto, SZ_ClubQuote>().ReverseMap();

            //CreateMap<SZ_QuoteDetailsComment, SZ_QuoteDetailsCommentDto>().ReverseMap();
            //CreateMap<SZ_QuoteDetailsCommentDto, SZ_QuoteDetailsComment>().ReverseMap();

            CreateMap<SZ_InventoryDto, SZ_Inventory>().ReverseMap();
            CreateMap<SZ_Inventory, SZ_InventoryDto>().ReverseMap();

            CreateMap<SZ_MasterCOA, SZ_MasterCOADto>().ReverseMap();
            CreateMap<SZ_MasterCOADto, SZ_MasterCOA>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
        }
    }
}
