using AcademyShopAPI.Models;
using DataLayer.Repository;
using DtoLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AcademyShopDBContext>();
//BusinessLayer
builder.Services.AddScoped<BusinessLayer.ManageBusiness>();
builder.Services.AddScoped<BusinessLayer.ManageUtenteBusiness>();
builder.Services.AddScoped<BusinessLayer.ManageProdottoBusiness>();
builder.Services.AddScoped<BusinessLayer.ManageOrdineBusiness>();
//DataLayer
builder.Services.AddScoped<DataLayer.ManageData>();
builder.Services.AddScoped<DataLayer.ManageUtenteData>();
builder.Services.AddScoped<DataLayer.ManageProdottoData>();
builder.Services.AddScoped<DataLayer.ManageOrdineData>();



// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add repositories
builder.Services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
builder.Services.AddScoped(typeof(IPutOrderRepository), typeof(PutOrderRepository));
builder.Services.AddScoped(typeof(IRepositoryGetOrdini), typeof(RepositoryGetOrdini));
builder.Services.AddScoped(typeof(IRepositoryWithDtoAsync<,>), typeof(RepositoryWithDtoAsync<,>));


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
