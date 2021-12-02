﻿using AutoMapper;
using Ordering.API.EventBusConsumer;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace Ordering.API.Mapping
{
    public class OrderingProfile : Profile
    {
        public OrderingProfile()
        {
            CreateMap<CheckoutOrderCommand,BasketCheckoutConsumer>().ReverseMap();
        }
    }
}
