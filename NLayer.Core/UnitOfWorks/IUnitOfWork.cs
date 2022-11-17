using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        //SaveChanges ifadesi kullanılana kadar değişiklikler memory'de tutulur. Bunun daha merkezi bir yönetimle ele alınması için UnitOfWork kullanılır
        Task CommitAsync();
        void Commit();
    }
}
