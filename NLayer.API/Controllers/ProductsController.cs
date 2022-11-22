using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Services; 

namespace NLayer.API.Controllers
{
    //[Route("api/[controller]/[action]")] yazılsaydı metodun ismi de yazılmak zorundaydı
    //[Route("api/[controller]")]
    //[ApiController]
    //[ValidateFilterAttribute]bütün controllerda tanımlama yapmak yerine program.cs'e eklendi
    public class ProductsController : CustomBaseController//ControllerBase
    {
        private readonly IMapper _mapper;
        //private readonly IService<Product> _service;
        private readonly IProductService _productService;

        //public ProductsController(IService<Product> service, IMapper mapper,IProductService productService)
        public ProductsController( IMapper mapper,IProductService productService)
        {
            //_service = service;
            _mapper = mapper;
            _productService = productService;
            //eğer _ işareti kullanılmasaydı this.productService=productService olarak yazılacaktı 

        }

        //[HttpGet("GetProductsWithcategory")]
        [HttpGet("[action]")]//metodun ismi değişiğinde otomatik olarak algılayacaktır
        public async Task<IActionResult> GetProductsWithCategory()
        {
            //aşağıda 3 satırda yapılan işlemler burada tek satıra indirilmiştir
            //mapping işlemi service içerisinde tamamlanmıştır
            return CreateActionResult(await _productService.GetProductsWithCategory());
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _productService.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());
            //return Ok( CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));//hem ok hem 200 vermek yerine optimize edildi
            //burada BadRequest NoContent gibi alternatifler de mevcut
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }

        [HttpGet("{id}")]//eğer burada id belirtilmezse alt satırdaki - int id - query string'den beklenir
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            //return Ok( CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));

            //bu kontrolün delete içinde de yapılması gerekiyor ve tekrara düşülüyor
            //if (product == null)
            //{
            //    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "bu id'ye sahip ürün bulunamadı"));
            //}
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productsDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _productService.AddAsync(_mapper.Map<Product>(productDto));
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productsDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)//createdDate'e ihtiyaç yok bu yüzden ProductUpdateDto kullanıldı
        {
            await _productService.UpdateAsync(_mapper.Map<Product>(productDto));
            //dönen bir data yok dolayısıyla mapleme yapılmadı
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _productService.GetByIdAsync(id);//ilerde kaldırılacak ve merkezi bir kontrol mekanizması eklenecek. 
            //exception fırlatılacak
            
            await _productService.RemoveAsync(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
