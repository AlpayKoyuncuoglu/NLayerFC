using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    //kullanıcıya açılması istenmeyen entity'let için dto'lar kullanılır. Mvc'deki ismi ViewModel'dir
    public class ProductDto : BaseDto
    {
        //burada navigation property'lere gerek yok

        //aşağıdaki gibi range ve required kullanılabilir ancak best practise için bu uygun değildir
        //çok sayıda property olduğunda yönetimi zorlaşacaktır
        //[Required(ErrorMessage ="")]
        public string Name { get; set; }
        //[Range(1,200)]
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
