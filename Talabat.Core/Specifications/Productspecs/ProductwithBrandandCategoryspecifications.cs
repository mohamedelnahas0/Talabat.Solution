using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications.Productspecs
{
    public class ProductwithBrandandCategoryspecifications : BaseSpecifications<Product>
    {
        public ProductwithBrandandCategoryspecifications(ProductSpescificationParams spescificationParams)
            : base(p =>
            (string.IsNullOrEmpty(spescificationParams.Search) || p.Name.ToLower().Contains(spescificationParams.Search))&&
            (!spescificationParams.BrandId.HasValue || p.BrandId == spescificationParams.BrandId.Value) &&
            (!spescificationParams.CategoryId.HasValue || p.CategoryId == spescificationParams.CategoryId.Value)
            )
        { 
            AddIncludes();


            if (!string.IsNullOrEmpty(spescificationParams.Sort))
            {
                switch (spescificationParams.Sort)
                {
                    case "PriceAsc":
                        //OrderBy = P => P.Price;
                        AddOrderBy(P => P.Price);
                        break;
                    case "PriceDesc":
                        //OrderByDesc = P => P.Price;
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;

                }
            }
            else
                AddOrderBy(P => P.Name);

            

            ApplyPagination((spescificationParams.PageIndex - 1) * spescificationParams.PageSize, spescificationParams.PageSize);


        }

        private void AddIncludes()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);

          
        }

        public ProductwithBrandandCategoryspecifications(int id)
            :base( P => P.Id == id ) 
        {
            AddIncludes();
        }
    }
}
