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
//IGenericRepository birden fazla entity alsayd�; IGenericRepository<,> say�ya g�re virg�ller eklenecekti
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddAutoMapper(typeof(MapProfile));//birden fazla mapping olabilir, MapProductProfile MapCategoryProfile gibi
//profile class'�ndan miras ald��� s�rece automapper gerekli b�t�n class'lar� bulu
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
