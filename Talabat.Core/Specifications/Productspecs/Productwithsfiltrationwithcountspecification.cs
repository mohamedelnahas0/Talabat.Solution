using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications.Productspecs
{
    public class Productwithsfiltrationwithcountspecification: BaseSpecifications<Product>
    {
        public Productwithsfiltrationwithcountspecification(ProductSpescificationParams spescificationParams)
            : base(p =>
            (string.IsNullOrEmpty(spescificationParams.Search) || p.Name.ToLower().Contains(spescificationParams.Search)) &&
            (!spescificationParams.BrandId.HasValue || p.BrandId == spescificationParams.BrandId.Value) &&
            (!spescificationParams.CategoryId.HasValue || p.CategoryId == spescificationParams.CategoryId.Value)
            )

        {



        }

        }
    }

