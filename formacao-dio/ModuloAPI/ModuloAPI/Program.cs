using Microsoft.EntityFrameworkCore;
using ModuloAPI.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AgendaContext>(options =>     // Adiconar um dbcontext do tipo agendacontext e passando algumas op��es
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));    // agendacontext, use o sqlserve e pegue a configura��o do appsettings, e pegue a chave conexaopadrao

// Adicionar a pol�tica de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Aplicar a pol�tica de CORS
app.UseCors("PermitirTudo");

app.MapControllers();

app.Run();