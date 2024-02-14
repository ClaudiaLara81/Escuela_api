using Escuela_Api.Datos;
using Escuela_Api.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Escuela_Api.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(AplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }
        public async Task Create(T entidad)
        {
            await dbSet.AddAsync(entidad);
            await Save();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
        public async Task<T> Obtain(Expression<Func<T, bool>> filtro = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> ObtainAll(Expression<Func<T, bool>>? filtro = null)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            return await query.ToListAsync();
        }

        public async Task Removed(T entidad)
        {
            dbSet.Remove(entidad);
            await Save();
        }


    }
}
