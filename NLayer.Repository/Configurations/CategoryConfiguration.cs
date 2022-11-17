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
    //dışardan erişim düşünülmediğinden internal olarak bırakıldı
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            //OnModelCreating içinde yapılan işlemler burada da yapılabilir
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn(1, 2);//id'nin kaçtan başlayacağı ve kaçar kaçar artacağı da bu şekilde belirlenir
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            builder.ToTable("Category");//DbSet'te belirtilen tablo ismi buradan değiştirilebilir//burada bir değer girilmediğinde default olarak dbset yanında tanımlanan değer alınır
        }
        //public void Configure(EntityTypeBuilder<Product> builder)
        //{

        //}
    }
}
