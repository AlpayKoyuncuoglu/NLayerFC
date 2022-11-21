using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class ProductUpdateDto
    {
        public int Id { get; set; }//sadece bu id için bir interface oluşturup customize edilebilir
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
