using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetProductsWithCategory()
        {
            //protected olarak belirlenen context nesnesine buradan erişilir
            //eager loading yapıldı. Data çekilirken category değerlerini alınması istenildi
            return await _context.Products.Include(x=>x.Category).ToListAsync();
        
        }

    }
}
