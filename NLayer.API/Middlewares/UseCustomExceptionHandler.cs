using Microsoft.AspNetCore.Diagnostics;
using NLayer.Core.DTOs;
using NLayer.Service.Exceptions;
using System.Text.Json;

namespace NLayer.API.Middlewares
{
    // static yapılmadığında hata verdi
    public static class UseCustomExceptionHandler
    {
        //IApplicationBuilder için bir extension yazılırsa,bunu implemnte etmiş tüm sınıflarda kullanılabilir
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                //run sistemi sonlandırıcı middleware'dir
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();//bu interface üzerinden uygulamada fırlatılan hata alınır
                    //fırlatılan exception tarafımdan yazılan mı uygulamanın default exceptionu mu anlamak adına ve
                    //tek bir noktada toplamak için service katmanı içinde exceptions klasörü oluşturuldu
                    var statusCode = exceptionFeature.Error switch
                    {
                        //clientSideException ise status'e 400 ata, değilse default olarak 500 ata
                        ClientSideException => 400,  
                        NotFoundException=>404,
                        _ => 500//bu daha çok db'ye bağlanmakla alakalı bir hatadır. Kullanıcıya dönmek doğru olmaz. Bir hata meydana geldi gibi bir genel ifade kullanılmalıdır
                            //burada 
                    };
                    context.Response.StatusCode = statusCode;
                    //var response = CustomResponseDto<NoContentDto>.Fail(context.Response.StatusCode);
                    var response = CustomResponseDto<NoContentDto>.Fail(statusCode,exceptionFeature.Error.Message);
                    //customBaseController ile datalar json olarak efcore sayesinde dönmektedir
                    //ancak burda dönüşümleri manuel olarak kendim yapmak zorundayım
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });

            });
        }
    }
}
