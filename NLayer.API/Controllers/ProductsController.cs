using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Services; 

namespace NLayer.API.Controllers
{
    //[Route("api/[controller]/[action]")] yazılsaydı metodun ismi de yazılmak zorundaydı
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController//ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IService<Product> _service;

        public ProductsController(IService<Product> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        //public Task<IActionResult> GetProductsWithCategory()
        //{

        //}

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _service.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());
            //return Ok( CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));//hem ok hem 200 vermek yerine optimize edildi
            //burada BadRequest NoContent gibi alternatifler de mevcut
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }

        [HttpGet("{id}")]//eğer burada id belirtilmezse alt satırdaki - int id - query string'den beklenir
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productsDto = _mapper.Map<ProductDto>(product);
            //return Ok( CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productsDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productsDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)//createdDate'e ihtiyaç yok bu yüzden ProductUpdateDto kullanıldı
        {
            await _service.UpdateAsync(_mapper.Map<Product>(productDto));
            //dönen bir data yok dolayısıyla mapleme yapılmadı
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _service.GetByIdAsync(id);//ilerde kaldırılacak ve merkezi bir kontrol mekanizması eklenecek. 
            //exception fırlatılacak
            //if(product == null)
            //{
            //    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404,"bu id'ye sahip ürün bulunamadı"));
            //}
            await _service.RemoveAsync(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
