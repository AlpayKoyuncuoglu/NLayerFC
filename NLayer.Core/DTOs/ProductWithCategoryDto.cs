using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class ProductWithCategoryDto : ProductDto
    {
        //service'ler api'nin istediği dtoları dönerken, repository'ler doğrudan entity dönmektedir
        public CategoryDto Category { get; set; }
    }
}
