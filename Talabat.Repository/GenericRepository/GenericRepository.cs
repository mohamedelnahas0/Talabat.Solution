using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Repository.Generic_Repository.Data;

namespace Talabat.Repository.Generic_Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region withoutspecifications


        public async Task<T?> GetbyIdAsync(int id)
        {
            //if (typeof(T) == typeof(Product))

            //    return await _dbContext.Set<Product>().Where(P => P.Id == id).Include(p => p.Brand).Include(P => P.Category).FirstOrDefaultAsync() as T;

            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            //if (typeof(T) == typeof(Product))
            //{
            //    return (IEnumerable<T>)await _dbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).AsNoTracking().ToListAsync();
            //}
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }



        #endregion










        public async Task<IReadOnlyList<T>> GetAllwithspecsAsync(ISpecifications<T> specifications)
        {
            return await ApplySepcification(specifications).AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetbyEntitywithspecsAsync(ISpecifications<T> specifications)
        {
            return await ApplySepcification(specifications).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsync(ISpecifications<T> specifications)
        {
            return await ApplySepcification(specifications).CountAsync();
        }


        private IQueryable<T> ApplySepcification(ISpecifications<T> specifications)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), specifications);
        }

        public async Task Add(T item) => await _dbContext.Set<T>().AddAsync(item);

        public void Delete(T item)=> _dbContext.Remove(item);
        

        

        public void Update(T item)=>_dbContext.Update(item);
        
    }
}
