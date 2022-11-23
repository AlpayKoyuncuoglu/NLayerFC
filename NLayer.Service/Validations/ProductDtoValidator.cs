using FluentValidation;
using NLayer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Validations
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            //propertyname ifadesi ile soldaki name direk buraya eşlenir
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} cannot null ").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Price).InclusiveBetween(1,int.MaxValue).WithMessage("{PropertyName} must be greater than 0");
            RuleFor(x => x.Stock).InclusiveBetween(1,int.MaxValue).WithMessage("{PropertyName} must be greater than 0");
           // RuleFor(x => x.CategoryId).InclusiveBetween(1,int.MaxValue).WithMessage("{PropertyName} must be greater than 0");
           //üst satır mvc için commentlendi
            //productDto içinde stock ve price değerlerinin default'u 0'dır. Bu yüzden notNull,notEmpty bir işe yaramaz
            //bu yüzden InclusiveBetween kullanmak mantıklıdır
            //int,double,float için değer tanımlamak gerekir. ancak string(name) gibi referans tipler için değer tanımlanmazsa default değer null olur
            //


        }
    }
}
