using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        //without specs
        Task<T?> GetbyIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();


        //withspecs
        Task<T?> GetbyEntitywithspecsAsync(ISpecifications<T> specifications);
        Task<IReadOnlyList<T>> GetAllwithspecsAsync(ISpecifications<T>specifications);

        Task<int> GetCountAsync(ISpecifications<T> specifications);

        Task Add(T item);

        void Delete(T item);
        void Update(T item);

    }
}
