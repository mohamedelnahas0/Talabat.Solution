using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.DTOS;
using Talabat.APIs.Errors;
using Talabat.Core;
using Talabat.Core.Entites.OrderAggregate;
using Talabat.Core.Services;
using Talabat.Service;
 
namespace Talabat.APIs.Controllers
{


    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IUnitofwork _unitofwork;

        public OrderController(IOrderService orderService, IMapper mapper , IUnitofwork unitofwork)
        {
            _orderService = orderService;
            _mapper = mapper;
            _unitofwork = unitofwork;
        }
        [ProducesResponseType(typeof(Order), 200)]
        [ProducesResponseType(typeof(Apiresponse), 400)]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var MappedAddress = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
            var Order = await _orderService.CreateOrderAsync(BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, MappedAddress);
            if (Order is null) return BadRequest(new Apiresponse(400, "There is a problem With You Order"));
            return Ok(Order);
        }
        [ProducesResponseType(typeof(IReadOnlyList<Order>), 200)]
        [ProducesResponseType(typeof(Apiresponse), 404)]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrderForUser()
            {
            var BuyerEmail =User.FindFirstValue(ClaimTypes.Email);
            var Orders = await _orderService.GetOrderForSpecificUserAsync(BuyerEmail);
            if (Orders is null) return NotFound(new Apiresponse(404, "There is no order for_ this user"));
            var mappedOrders = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(Orders);
            return Ok(mappedOrders);

            }



        [ProducesResponseType(typeof(Order), 200)]
        [ProducesResponseType(typeof(Apiresponse), 404)]
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Order>> GetOrderForUserById(int Id)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var Order = await _orderService.GetOrderByIdForSpecificUserAsync(BuyerEmail, Id);
            if (Order is null) return NotFound(new Apiresponse(404, $"There is no order  with id ={Id} for this user"));
            var Mappedordes = _mapper.Map<Order, OrderToReturnDto>(Order);
            return Ok(Mappedordes);

        }

        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var DeliveryMethos =await _unitofwork.Repository<DeliveryMethod>().GetAllAsync();
            return Ok(DeliveryMethos);
        }

    }

}
