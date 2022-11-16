using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(int id);
        //IQueryable ile yazılan metod doğrudan veritabanına gitmez. Öncelikle ToList() gibi metodların eklenmesi gerekir.
        //bu araya orderBy gibi bir metod eklenebilir. Bu sayede veri tamamen çekilmeden önce order yapılır
        //Önce ToList() denilip sonra orderBy yapıldması bir performans kaybıdır
        //--
        //efcore'da sorguların tamamı expression delegesi alır
        //action,function bir delegedir ve metodları işaret ederler
        //burada T alıp, bool dönüşü yapılmaktadır
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T Entity);
        //Birden fazla ekleme işlemi yapmak için AddRange kullanılır. Burada da List yerine IEnumerable kullanıldı. Mümkün olduğunca soyut nesnelerle çalışmak önemli
        Task AddRangeAsync(IEnumerable<T> entites);
        //update ve delete için efcore'da async yoktur. Memory'e alınan class'ın sadece state'i değiştirildiği içinde, uzun süren bir işlem olmadığı için gerek de yoktur.
        void Update(T Entity);
        void Remove(T Entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
