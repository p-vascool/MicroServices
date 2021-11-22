using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    public class GetOrdersList : IRequest<List<OrdersVm>>
    {
        public string UserName { get; set; }
        public GetOrdersList(string userName)
        {
            this.UserName = userName;
        }
    }
}
