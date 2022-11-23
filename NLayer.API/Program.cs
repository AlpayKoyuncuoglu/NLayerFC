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
//addFluentValidation eklendi ama devamýnda yer ifadesi de yapýlmalý
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;//invalid filter'ý baskýla
    //mvc'de buna gerek yok ancak apide default olarak böyle bir süreç olduðu için baskýlama gerekiyor
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

builder.Services.AddScoped(typeof(NotFoundFilter<>));
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//repoServiceModule'de yapýlan deðiþikliklerle üst satýr kodlar yorum satýrýna alýnmýþtýr

//IGenericRepository birden fazla entity alsaydý; IGenericRepository<,> sayýya göre virgüller eklenecekti
//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//repoServiceModule'de yapýlan deðiþikliklerle üst satýr kodlar yorum satýrýna alýnmýþtýr

//builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
//repoServiceModule'de yapýlan deðiþikliklerle üst satýr kodlar yorum satýrýna alýnmýþtýr

builder.Services.AddAutoMapper(typeof(MapProfile));//birden fazla mapping olabilir, MapProductProfile MapCategoryProfile gibi
//profile class'ýndan miras aldýðý sürece automapper gerekli bütün class'larý bulur
//buradaki kod kirliliði otofac ile düzeltilecektir

//repoServiceModule'de yapýlan deðiþikliklerle aþaðýdaki kodlar yorum satýrýna alýnmýþtýr
//builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<IProductService, ProductService>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
//builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddDbContext<AppDbContext>(x =>
 {
     //daha önce startup içindeki configuration'un yazýmý bu þekilde deðiþmiþtir
     //bu kod ile appsettings.json içindeki SqlConnection kullanýlmýþ olur
     //GetConnectionString=>connection string'i bul
     //SqlConnection=>appSettingsJson içinde tanýmlanan connectionString deðeri
     x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
     {
         //option.MigrationsAssembly("NLayer.Respository");bu kullaným da doðrudur ancak tip güvenli yapmak için aþaðýdaki gibi güncellendi;
         //olasý bir isim deðiþikliðinde hatayý yakalamak böylelikle kolaylaþmýþtýr   
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
