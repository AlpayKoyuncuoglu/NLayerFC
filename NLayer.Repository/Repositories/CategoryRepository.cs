﻿using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Category> GetSingleCategoryByIdWithProductsAsync(int categoryId)
        {
            //aynı id değerinden birden fazla olduğunu varsayalım
            //eğer firstOrDefault kullanılsaydı ilk bulduğunu getirirdi
            //ancak singleOrDefault birden fazla aynı Id'den olması durumunda hata fırlatır
            //verideki tutarsızlık tespit edilmiş olur
            return await _context.Categories.Include(x => x.Products).Where(x => x.Id == categoryId).SingleOrDefaultAsync();
        }
    }
}
