using AutoMapper;
using MelodiaxGuitarsAPI.DTOs;
using MelodiaxGuitarsAPI.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace MelodiaxGuitarsAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Brand, BrandDto>();
            CreateMap<CartItem, CartItemDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderProduct, OrderProductDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<ShoppingCart, ShoppingCartDto>();
            CreateMap<User, UserDto>();

            CreateMap<BrandDto, Brand>();
            CreateMap<CartItemDto, CartItem>();
            CreateMap<CategoryDto, Category>();
            CreateMap<OrderDto, Order>();
            CreateMap<OrderProductDto, OrderProduct>();
            CreateMap<ProductDto, Product>();
            CreateMap<ShoppingCartDto, ShoppingCart>();
            CreateMap<UserDto, User>();
        }
    }
}
