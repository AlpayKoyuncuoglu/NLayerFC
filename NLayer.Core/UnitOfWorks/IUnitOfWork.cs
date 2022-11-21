using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        //tek bir transaction üzerinden işlem yapılması sağlanır
        //SaveChanges ifadesi kullanılana kadar değişiklikler memory'de tutulur.  
        //2 farklı repository üzerinden işlemler yapıldığında biri başarılı, diğeri başarısız olabilir.
        //Tutarsız işlemleri önlemek için merkezi bir yönetimle ele alınması gerekir ve UnitOfWork kullanılır
        //eğer bir repository bile hata verse, bütün değişiklikler bu kanalla geri alınır
        Task CommitAsync();
        void Commit();
    }
}
