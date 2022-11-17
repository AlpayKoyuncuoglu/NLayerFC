using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class// class ifadesi belirtilmezse hata verir. 
        //Burada t için int de gönderilmesi gibi bir ihtimal doğar//efcore burada class ister
    {
        protected readonly AppDbContext _context;//product'la ilgili category bilgileri alınmak istendiğinde productRepository'e ihtiyaç duyulur.
        //Bu yüzden miras alınacak yerlerde buradaki AppDbContext'e ihtiyaç duyulacaktır. protected erişim belirleyicisinin kullanılma sebebi budur
        private readonly DbSet<T> _dbSet;
        //readonly ile, ya tanımlama yapılırken ya da constructor içinde değer atanabilir
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task AddAsync(T Entity)
        {
            await _dbSet.AddAsync(Entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entites)
        {
            await _dbSet.AddRangeAsync(entites);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            //AsNoTracking çektiği dataları memory'e almaması için kullanılır. Kullanılmazsa çok fazla data çekildiğinde, dataların anlık durumu check edilecektir(dispose edilene kadar)
            //Hız yavaşlar//update, insert, delete gibi işlemler yapılmayacağından bu kullanım daha performanslıdır
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            //FindAsync PrimaryKey bekler. Birden fazla PrimaryKey atanabileceğinden, birden fazla gönderim de yapılabilir.
            return await _dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
            //_context.Entry(entity).State=EntityState.Deleted;//2'si de aynı işlevdedir
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            //removeRange burada foreach ile döner
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
    }
}
