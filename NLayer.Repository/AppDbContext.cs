﻿using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using NLayer.Repository.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository
{
    //pcm içinde kod yazabilmek için EntityFrameworkCore.Tools kullanılır
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //aşağıda dbset olmaksızın product üzerinden ProductFeature eklenmek istenirse bu şekilde bir kullanım yapılabilir;
            //bu commentli kullanım best practise açısından daha olumludur
            //var p = new Product() { ProductFeature = new ProductFeature() { } };
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.Entity)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                entityReference.UpdatedDate = DateTime.Now;
                                break;
                            }
                    }
                }
            }
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;

                                entityReference.UpdatedDate = DateTime.Now;
                                break;
                            }
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Id değerleri Primary ve ForeignKey olarak EntityFramework'ün algılayabileceği, uyumlu şekilde yapılmıştır.
            //Ancak key değerleri configure edilmek istenirse bu metod içinde değişiklik yapılabilir.

            //Bu yaklaşım fluentApi olarak adlandırılır. 
            //modelBuilder.Entity<Category>().HasKey(x=>x.Id)//HasName() ile property ismi de değiştirilebilir
            //best practise açısından bu tarz ayarlamalar başka bir class içinde yapılmalıdır

            //bütün classLibrary'ler bir assembly'dir
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());//bütün assembly'ler içindeki configuration dosyalarını oku
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());//configuration'lar tek tek yapılmak istendiğinde bu şekilde yazılabilir

            //farklılığı görmek adına ProductFeature burada eklenmiştir
            modelBuilder.Entity<ProductFeature>().HasData(
            new ProductFeature()
            {
                Id = 1,
                Color = "Kırmızı",
                Height = 100,
                Width = 200,
                ProductId = 1
            },
            new ProductFeature()
            {
                Id = 2,
                Color = "Mavi",
                Height = 300,
                Width = 500,
                ProductId = 2
            }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
