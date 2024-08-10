using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Specifications;

namespace Talabat.Repository.Generic_Repository
{
    public static class SpecificationEvaluator<T> where T : BaseEntity
    {
        //function to build query dynamic
        public static IQueryable<T> GetQuery(IQueryable<T> InputQuery, ISpecifications<T> specifications)
        {
            var Query = InputQuery;

            if (specifications.Criteria is not null)

                Query = Query.Where(specifications.Criteria);


            if (specifications.OrderBy is not null)

                Query = Query.OrderBy(specifications.OrderBy);


            else if (specifications.OrderByDesc is not null)

                Query = Query.OrderByDescending(specifications.OrderByDesc);



            if (specifications.IsPaginationinabled)
                Query = Query.Skip(specifications.Skip).Take(specifications.Take);

            Query = specifications.Includes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));


            return Query;
        }
    }
}
