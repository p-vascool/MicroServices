using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersList, List<OrdersVm>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            this._orderRepository = orderRepository;
            this._mapper = mapper;
        }

        public async Task<List<OrdersVm>> Handle(GetOrdersList request, CancellationToken cancellationToken)
        {
            var orderList = await _orderRepository.GetOrdersByUsername(request.UserName);
            return _mapper.Map<List<OrdersVm>>(orderList);
        }
    }
}
