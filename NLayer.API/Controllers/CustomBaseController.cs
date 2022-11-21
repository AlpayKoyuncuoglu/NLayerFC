using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;

namespace NLayer.API.Controllers
{
    //miras alınan yerde mevcut
    //[Route("api/[controller]")]
    //[ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction]//swagger'ın bunu bir endpoint olarak algılamasının önüne geçildi
        //get ve post'u olmadığından hata fırlatacaktı
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if (response.StatusCode == 204)
            {
                return new ObjectResult(null)
                { StatusCode = response.StatusCode };
            };
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
