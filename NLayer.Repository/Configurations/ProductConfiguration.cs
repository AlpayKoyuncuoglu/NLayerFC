using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Stock).IsRequired();
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");//virgüle kadar 16, virgülden sonra 2 karakter
            builder.ToTable("Products");
            //efcore aşağıdaki ifadeyi kendi algılayacaktır. Ancak açık şekilde işlkemin görülmesi için yazıldı
            builder.HasOne(x => x.Category).WithMany(x=>x.Products).HasForeignKey(x=>x.CategoryId);//bir product'ın bir category'si olabilir. Bir category'nin birden çok product'ı

        }
    }
}
