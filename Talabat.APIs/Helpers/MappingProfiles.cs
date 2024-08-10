using AutoMapper;
using Talabat.APIs.DTOS;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Basket;
using Talabat.Core.Entites.Identity;
using Talabat.Core.Entites.OrderAggregate;
using static System.Net.WebRequestMethods;
using OrderAddress = Talabat.Core.Entites.OrderAggregate.Address;
using IdentityAddress = Talabat.Core.Entites.Identity.Address;


namespace Talabat.APIs.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProducttoreturnDto>()
                .ForMember(d => d.Brand, O => O.MapFrom(S => S.Brand.Name))
                .ForMember(d => d.Category, O => O.MapFrom(S => S.Category.Name))
                .ForMember(P => P.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());

            CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
            CreateMap<BasketItemDto , BasketItem>().ReverseMap();
            CreateMap<IdentityAddress, AddressDto>().ReverseMap();
            CreateMap<AddressDto, OrderAddress>().ReverseMap();
           // CreateMap<AddressDto, Address>();
           CreateMap<Order , OrderToReturnDto>()
                .ForMember(d=> d.DeliveryMethod ,O => O.MapFrom(S=> S.DeliveryMethod.Shortname))
                .ForMember(d=> d.DeliveryMethodCost ,O => O.MapFrom(S=> S.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()

                .ForMember(d => d.ProductId, O => O.MapFrom(S => S.product.ProductId))
                .ForMember(d => d.ProductName, O => O.MapFrom(S => S.product.ProductName))
                .ForMember(d => d.PictureUrl, O => O.MapFrom<OrderItemPictureUrlResolver>());




        }

    }
}
