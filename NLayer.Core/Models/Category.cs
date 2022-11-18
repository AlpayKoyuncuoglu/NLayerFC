using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        //Category'den Product'a bağlantı sağladığı için alttaki property bir navigation property'dir
        public ICollection<Product> Products{ get; set; }
    }
}
