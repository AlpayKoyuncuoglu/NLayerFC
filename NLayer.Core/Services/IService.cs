using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IService<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        //Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T Entity);
        Task AddRangeAsync(IEnumerable<T> entites);
        void UpdateAsync(T Entity);
        void RemoveAsync(T Entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
