using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entites;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Generic_Repository;
using Talabat.Repository.Generic_Repository.Data;

namespace Talabat.Repository
{
    public class Unitofwork : IUnitofwork
    {
        private readonly StoreContext _dbcontext;
        private Hashtable _repositories ;

        public Unitofwork(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;
            _repositories = new Hashtable();
        }
        public async Task<int> CompleteAsync() => await _dbcontext.SaveChangesAsync();

        public async ValueTask DisposeAsync()  =>  await _dbcontext.DisposeAsync();
                
                

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;
            if(!_repositories.ContainsKey(type))   
            {
                var Repository = new GenericRepository<TEntity>(_dbcontext);
                _repositories.Add(type, Repository);
            }
            return _repositories[type] as IGenericRepository<TEntity> ;
           

        }
    }
} 
