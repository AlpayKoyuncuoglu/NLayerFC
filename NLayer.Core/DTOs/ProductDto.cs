using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    //kullanıcıya açılması istenmeyen entity'let için dto'lar kullanılır. Mvc'deki ismi ViewModel'dir
    public class ProductDto : BaseDto
    {
        //burada navigation property'lere gerek yok
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
