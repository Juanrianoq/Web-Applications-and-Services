using API_First_Proyect.DAL;
using API_First_Proyect.Domain.Interfaces;
using API_First_Proyect.Domain.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Esta linea me crea el contexto de la BD a la hora de correr esta API.
builder.Services.AddDbContext<DataBaseContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregamos la dependencia para conectar el servicio "Country y State" a la base de datos.
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddTransient<SeederDB>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

SeederData();

void SeederData()
{
     IServiceScopeFactory? scopedFactory = app.Services.GetService<IServiceScopeFactory>();

     using (IServiceScope? scope = scopedFactory.CreateScope())
     {
          SeederDB? service = scope.ServiceProvider.GetService<SeederDB>();
          service.SeederAsync().Wait();
     }
}

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
