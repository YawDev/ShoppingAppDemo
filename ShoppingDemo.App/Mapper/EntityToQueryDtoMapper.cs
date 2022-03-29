using Shopper.App.Models;
using ShoppingDemo.App.Data.Entites;
using ShoppingDemo.App.Data.Enums;

namespace ShoppingDemo.App.Mapping
{
    public class EntityToQueryDtoMapper : AutoMapper.Profile
    {
        public EntityToQueryDtoMapper()
        {
            CreateMap<Item, ItemModel>();
            CreateMap<OrderItem, OrderItemModel>();
            CreateMap<Order, PlaceOrderModel>();
            CreateMap<Order, OrderModel>()
              .ForMember(x => x.FirstName, src => src.MapFrom(s => s.Customer.FirstName))
              .ForMember(x => x.LastName, src => src.MapFrom(s => s.Customer.LastName))
              .ForMember(x => x.Email, src => src.MapFrom(s => s.Customer.Email))
              .ForMember(dest => dest.Status, opt => opt.MapFrom(x => System.Enum.GetName(typeof(OrderStatus), x.Status)));


            CreateMap<OrderItem, OrderItemModel>();
            CreateMap<OrderModel, PlaceOrderModel>();
            CreateMap<ShoppingCartItemModel, OrderItemModel>();
            CreateMap<Item, EditItemModel>();
            CreateMap<ShoppingCartItem, ShoppingCartItemModel>();
            CreateMap<ShoppingCart, ShoppingCartModel>();
            CreateMap<PaymentCard, CardModel>();
            CreateMap<ShippingAddress, AddressModel>();
            CreateMap<BillingAddress, AddressModel>();
            CreateMap<AccountModel, ApplicationUser>();
            CreateMap<PlaceOrderModel, Order>();
            CreateMap<AddressModel, ShippingAddress>();
            CreateMap<AddressModel, BillingAddress>();
            CreateMap<CardModel, CardInformation>();
            CreateMap<CardInformation, CardInfoModel>();


        }
    }
}