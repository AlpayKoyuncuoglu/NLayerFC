using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;

namespace NLayer.API.Filters
{
    //filterların ilerisi için kullanılan şey interceptor ve middleware'dir
    public class ValidateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //service katmanında valitaionla yazılmış olan kontroller buraya doğrudan entegre
            //burada ModelState'e doğrudan bir mapleme yapılmaktadır, framework ile
            //fluentvalidation ile yapılmasa  da hatalar context'in modelstate'e yüklenir
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                context.Result = new BadRequestObjectResult(CustomResponseDto<NoContentDto>.Fail(400, errors));
                //errors.ToList().ForEach(x =>
            }
        }
    }
}
