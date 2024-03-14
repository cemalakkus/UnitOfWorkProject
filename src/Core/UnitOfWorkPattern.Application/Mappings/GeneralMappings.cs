using AutoMapper;
using UnitOfWorkPattern.Application.Dtos;
using UnitOfWorkPattern.Domain.Entities;

namespace UnitOfWorkPattern.Application.Mappings;

public class GeneralMappings : Profile
{
    public GeneralMappings()
    {
        CreateMap<Product, ProductCreateRequest>().ReverseMap();
        CreateMap<Product, GetAllProductsResponse>().ReverseMap();
        CreateMap<Product, GetProductByIdResponse>().ReverseMap();
        CreateMap<GetAllProductsRequest, GetAllProductsParameter>();
    }
}
