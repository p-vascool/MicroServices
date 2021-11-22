using Discount.Grpc.Protos;
using System.Threading.Tasks;

namespace Basket.API.GrpcsServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _grpcClient;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProto)
        {
            this._grpcClient = discountProto;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName };

            return await _grpcClient.GetDiscountAsync(discountRequest);
        }
    }
}
