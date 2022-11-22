using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    //[ValidateFilterAttribute]bütün controllerda tanımlama yapmak yerine program.cs'e eklendi
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //[HttpGet("[action]/{categoryId}")]// api/categories/GetSingleCategoryByIdWithProducts/2
        [HttpGet("[action]/{categoryId}")]//   int categoryId, bu ifadeyle aynı olmalı
        public async Task<IActionResult> GetSingleCategoryByIdWithProducts(int categoryId)
        {
            return CreateActionResult(await _categoryService.GetSingleCategoryWithProductsAsync(categoryId));
        }
    }
}
