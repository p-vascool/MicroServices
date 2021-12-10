using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;
using System.Net;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Controllers
{
    public class ShoppingController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IBasketService _basketService;
        private readonly ICatalogService _catalogService;

        public ShoppingController(IOrderService orderService, IBasketService basketService, ICatalogService catalogService)
        {
            this._orderService = orderService;
            this._basketService = basketService;
            this._catalogService = catalogService;
        }

        [HttpGet("{username}", Name = "GetShopping")]
        [ProducesResponseType(typeof(ShoppingModel),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingModel>> GetShopping(string username)
        {
            var basket = await _basketService.GetBasket(username);

            foreach (var item in basket.Items)
            {
                var product = await _catalogService.GetCatalog(item.ProductId);
                item.ProductName = product.Name;
                item.Category = product.Category;
                item.Summary = product.Summary;
                item.Description = product.Description;
                item.ImageFile = product.ImageFile;
            }

            var order = await _orderService.GetOrdersByUserName(username);

            var shoppingModel = new ShoppingModel
            {
                Username = username,
                BasketWithProducts = basket,
                Orders = order
            };

            return Ok(shoppingModel);
        }
    }
}
