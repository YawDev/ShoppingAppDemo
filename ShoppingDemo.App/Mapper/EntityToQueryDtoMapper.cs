using Shopper.App.Models;
using ShoppingDemo.App.Data.Entites;

namespace ShoppingDemo.App.Mapping
{
    public class EntityToQueryDtoMapper : AutoMapper.Profile
    {
        public EntityToQueryDtoMapper()
        {
            CreateMap<Item, ItemModel>();
            CreateMap<OrderItem, OrderItemModel>();
            CreateMap<Order, PlaceOrderModel>();
            CreateMap<Order, OrderModel>();
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