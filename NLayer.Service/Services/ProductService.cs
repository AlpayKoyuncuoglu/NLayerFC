using AutoMapper;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    //IProductService IService'i miras almasına rağmen burada neden Service<Product>'tan da miras alındı. Service<Product> de IService'den miras almaktadır
    //Service içerisinde IService'den gelen metodların içi doldurulmuştur.
    //Sadece IService'den miras alınsaydı burada pek çok metodun içi tekrar doldurulacaktı
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        //repoların ve service'lerin dönüş tiplerinin değiştiği gözlenmektedir. Repoda dönüş tipi böyleyken: Task<List<Product>> aşağıda;
        public async Task<List<ProductWithCategoryDto>> GetProductsWithCategory()
        {
            //ilerde try catch burada kullanılacaktır
            var products = await _productRepository.GetProductsWithCategory();
            var productsDto = _mapper.Map<List<ProductWithCategoryDto>>(products);
            return productsDto;
        }
    }
}
