using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VehicleStateCodes.Data.Domein.Data;
using VehicleStateCodes.Data.Domein.Domein;

namespace VehicleStateCodes.DataBase.GenericRepository
{
    public class GeneralRepository<TSource> : IGeneralRepository<TSource> where TSource : class, IGlobalId
    {
        private readonly Context _context;
        private readonly DbSet<TSource> _entities;

        public GeneralRepository(Context context)
        {
            _context = context;
            _entities = _context.Set<TSource>();
        }

        public async Task Delete(TSource entity)
        {
            _entities.Remove(entity);
        }
        public async Task DeleteById(int id)
        {
            _context.Remove(id);
        }



        public async Task<List<TSource>> GetAll(Expression<Func<TSource, bool>> predicate)
        {
            return await _entities.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<TSource> GetAsync(Expression<Func<TSource, bool>> predicate)
        {
            return await _entities.AsNoTracking().Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<TSource> AddAsync(TSource entity)
        {
            var Tresult = await _entities.AddAsync(entity);
            return Tresult.Entity;
        }
        public async Task<IEnumerable<TSource>> OrderBy<Tkey>(Expression<Func<TSource, Tkey>> predicate)
        {
            return await _entities.AsNoTracking().OrderByDescending(predicate).ToListAsync();

        }

        public async Task Update(TSource entity)
        {
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

        }

        public IQueryable<TSource> Where(Expression<Func<TSource, bool>> predicate)
        {
            return _entities.AsNoTracking().Where(predicate);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync();
        }
    }
}
