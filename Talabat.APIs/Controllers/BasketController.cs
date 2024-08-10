using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOS;
using Talabat.APIs.Errors;
using Talabat.Core.Entites.Basket;
using Talabat.Core.Repositories.Contract;

namespace Talabat.APIs.Controllers
{
   
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
        var basket = await _basketRepository.GetBasketAsync(id);
           return Ok(basket is null ? new CustomerBasket (id) : basket);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var mappedbasket = _mapper.Map<CustomerBasketDto , CustomerBasket>(basket);
            var createdorupadted = await _basketRepository.UpdateBasketAsync(mappedbasket);
            if (createdorupadted is null) return BadRequest(new Apiresponse(400));
            return Ok(createdorupadted);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
            return await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
