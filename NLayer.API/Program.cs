using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NLayer.API.Filters;
using NLayer.API.Middlewares;
using NLayer.API.Modules;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using NLayer.Service.Mapping;
using NLayer.Service.Services;
using NLayer.Service.Validations;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//services
builder.Services.AddControllers(
    options =>
    {
        options.Filters.Add(new ValidateFilterAttribute());
    }
    ).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());
//addFluentValidation eklendi ama devam�nda yer ifadesi de yap�lmal�
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;//invalid filter'� bask�la
    //mvc'de buna gerek yok ancak apide default olarak b�yle bir s�re� oldu�u i�in bask�lama gerekiyor
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

builder.Services.AddScoped(typeof(NotFoundFilter<>));
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//repoServiceModule'de yap�lan de�i�ikliklerle �st sat�r kodlar yorum sat�r�na al�nm��t�r

//IGenericRepository birden fazla entity alsayd�; IGenericRepository<,> say�ya g�re virg�ller eklenecekti
//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//repoServiceModule'de yap�lan de�i�ikliklerle �st sat�r kodlar yorum sat�r�na al�nm��t�r

//builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
//repoServiceModule'de yap�lan de�i�ikliklerle �st sat�r kodlar yorum sat�r�na al�nm��t�r

builder.Services.AddAutoMapper(typeof(MapProfile));//birden fazla mapping olabilir, MapProductProfile MapCategoryProfile gibi
//profile class'�ndan miras ald��� s�rece automapper gerekli b�t�n class'lar� bulur
//buradaki kod kirlili�i otofac ile d�zeltilecektir

//repoServiceModule'de yap�lan de�i�ikliklerle a�a��daki kodlar yorum sat�r�na al�nm��t�r
//builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<IProductService, ProductService>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
//builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddDbContext<AppDbContext>(x =>
 {
     //daha �nce startup i�indeki configuration'un yaz�m� bu �ekilde de�i�mi�tir
     //bu kod ile appsettings.json i�indeki SqlConnection kullan�lm�� olur
     //GetConnectionString=>connection string'i bul
     //SqlConnection=>appSettingsJson i�inde tan�mlanan connectionString de�eri
     x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
     {
         //option.MigrationsAssembly("NLayer.Respository");bu kullan�m da do�rudur ancak tip g�venli yapmak i�in a�a��daki gibi g�ncellendi;
         //olas� bir isim de�i�ikli�inde hatay� yakalamak b�ylelikle kolayla�m��t�r   
         option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
     });

 }
);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));
//middlewares

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCustomException();

app.UseAuthorization();

app.MapControllers();

app.Run();
