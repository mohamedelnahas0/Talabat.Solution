using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOS;
using Talabat.APIs.Errors;
using Talabat.Core.Entites.Basket;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{
    [Authorize]
    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentService paymentService , IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }
        [ProducesResponseType(typeof(CustomerBasketDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Apiresponse),StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentId(string basketId)
        {
            var customerbasket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            if (customerbasket is null) return BadRequest(new Apiresponse(400, "There is a problem in your Basket"));
            var mappedbasket = _mapper.Map<CustomerBasket , CustomerBasketDto>(customerbasket);
            return Ok(mappedbasket);

        }

    }
}
