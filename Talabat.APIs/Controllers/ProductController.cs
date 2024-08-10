using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOS;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core;
using Talabat.Core.Entites;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Productspecs;

namespace Talabat.APIs.Controllers
{

    public class ProductController : BaseApiController
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;

        public ProductController(
          IUnitofwork unitofwork
            , IMapper mapper)

        {
           
           _unitofwork = unitofwork;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProducttoreturnDto>>> GetProducts([FromQuery]ProductSpescificationParams spescificationParams)
        {
            var specs = new ProductwithBrandandCategoryspecifications(spescificationParams);
            var products = await _unitofwork.Repository<Product>().GetAllwithspecsAsync(specs);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProducttoreturnDto>>(products);


            var countspec = new Productwithsfiltrationwithcountspecification(spescificationParams);
            var count = await _unitofwork.Repository<Product>().GetCountAsync(countspec);

            return Ok(new Pagination<ProducttoreturnDto>(spescificationParams.PageIndex,spescificationParams.PageSize, count, data));
        }

        [ProducesResponseType(typeof(ProducttoreturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Apiresponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProducttoreturnDto>> GetProduct(int id)
        {
            var specs = new ProductwithBrandandCategoryspecifications(id);
            var product = await _unitofwork.Repository<Product>().GetbyEntitywithspecsAsync(specs);
            if (product == null)
            {
                return NotFound(new Apiresponse(404)); //404
            }
            return Ok(_mapper.Map<Product, ProducttoreturnDto>(product));


             
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _unitofwork.Repository<ProductBrand>().GetAllAsync();
            return Ok(brands);
        }


        [HttpGet("Categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCatrgories()
        {
            var categotriess = await _unitofwork.Repository<ProductCategory>().GetAllAsync();
            return Ok(categotriess);
        }



    }
}
 