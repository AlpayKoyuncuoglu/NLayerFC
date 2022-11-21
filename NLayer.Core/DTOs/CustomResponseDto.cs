using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    //tek bir response dto olması client'ın kullanımı açısından kolaylık sağlayacaktır
    //istenirse 2 ayrı class tanımlaması yapılıp başarılı ve başarısız durumlar için gerekli olan çağrılabilir.
    //Ancak aynı property isimlerinin kullanılmasına dikkat edilmelidir


    //public class CustomResponseDto enttiy almayacak şekilde birebir aynı isimle ve altı tamamen aynı olarak  tanımlanabilir
    //geriye bir şey dönülmeyeceğinde üstteki, değilse alttaki kullanılabilir

    public class CustomResponseDto<T>
    {
        public T Data { get; set; }
        [JsonIgnore]//respoense'ın body'sinde dönmeye gerek yoksa ve kod içerisinde lazımsa JsonIgnore kullanılır
        public int StatusCode { get; set; }
        public List<String> Errors { get; set; }
        //static factory method; static factory method design pattern
        //nesne oluşturmaksızın metodların çalıştırılması
        public static CustomResponseDto<T> Success(int statusCode, T data)
        {
            return new CustomResponseDto<T> { Data = data, StatusCode = statusCode };
        }
        public static CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode };
        }
        public static CustomResponseDto<T> Fail(int statusCode, List<string> errors)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = errors };
        }
        public static CustomResponseDto<T> Fail(int statusCode, string error)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors =new List<String> { error } };
        }

    }
}
