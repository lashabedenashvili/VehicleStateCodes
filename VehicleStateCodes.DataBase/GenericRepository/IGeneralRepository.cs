using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VehicleStateCodes.Data.Domein.Data;

namespace VehicleStateCodes.DataBase.GenericRepository
{
    public interface IGeneralRepository<TSource> where TSource : class,IGlobalId
    {
        Task<TSource> AddAsync(TSource entity);
        Task Update(TSource entity);
        Task Delete(TSource entity);
        Task DeleteById(int id);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<TSource> GetAsync(Expression<Func<TSource, bool>> predicate);
        Task<IEnumerable<TSource>> OrderBy<Tkey>(Expression<Func<TSource, Tkey>> predicate);
        IQueryable<TSource> Where(Expression<Func<TSource, bool>> predicate);
        Task<List<TSource>> GetAll(Expression<Func<TSource, bool>> predicate);
        public IQueryable<TSource> AsQuareble();



    }
}
