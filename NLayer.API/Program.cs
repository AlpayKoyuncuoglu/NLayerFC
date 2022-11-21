using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using NLayer.Service.Mapping;
using NLayer.Service.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//services
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//IGenericRepository birden fazla entity alsaydý; IGenericRepository<,> sayýya göre virgüller eklenecekti
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddAutoMapper(typeof(MapProfile));//birden fazla mapping olabilir, MapProductProfile MapCategoryProfile gibi
//profile class'ýndan miras aldýðý sürece automapper gerekli bütün class'larý bulu
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

//middlewares
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
