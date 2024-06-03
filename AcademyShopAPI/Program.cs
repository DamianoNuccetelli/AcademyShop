using AcademyShopAPI.Models;
using DataLayer.Repository;
using DtoLayer;

// Add CORS
var ReactSpecificOrigins = "enablecorsAcademyShop";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AcademyShopDBContext>();
//BusinessLayer
builder.Services.AddScoped<BusinessLayer.ManageUtenteBusiness>();
builder.Services.AddScoped<BusinessLayer.ManageProdottoBusiness>();
builder.Services.AddScoped<BusinessLayer.ManageOrdineBusiness>();
//DataLayer
builder.Services.AddScoped<DataLayer.ManageUtenteData>();
builder.Services.AddScoped<DataLayer.ManageProdottoData>();
builder.Services.AddScoped<DataLayer.ManageOrdineData>();



// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add repositories
builder.Services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
builder.Services.AddScoped(typeof(IRepositoryOrdine), typeof(RepositoryOrdine));
builder.Services.AddScoped(typeof(IRepositoryUtente), typeof(RepositoryUtente));
builder.Services.AddScoped(typeof(IRepositoryProdotto<Prodotto>), typeof(RepositoryProdotto));

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: ReactSpecificOrigins,
                           builder =>
                           {
                               builder.WithOrigins("http://localhost:3000")
                       .AllowAnyHeader()
                       .AllowAnyMethod();
                           });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseStaticFiles(); //Da usare in futuro -Gabriele
//app.UseRouting();

// CORS
app.UseCors(ReactSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
