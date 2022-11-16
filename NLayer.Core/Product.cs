using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core
{
    //class içindeki property, field'ların default erişim belirleyicisi private'tır
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        //entityframework buradaki CategoryId değerini otomatik olarak foreign key olarak algılar
        //ancak yazım Category_Id olarak değişse [ForeignKey("Category_Id")] ifadesinin üste eklenmesi gerekir
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ProductFeature ProductFeature { get; set; }
    }
}
