using AutoMapper;
using AutoMapper.Execution;
using Talabat.APIs.DTOS;
using Talabat.Core.Entites;

namespace Talabat.APIs.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProducttoreturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProducttoreturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            
                return $"{_configuration["ApiBAseUrl"]}/{source.PictureUrl}";

                return string.Empty ;

            
        }
    }
}
