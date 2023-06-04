using MediatR;
using Microsoft.OpenApi.Models;
using Questao5.Domain.Handlers;
using Questao5.Infrastructure.Database.CommandStore.ContaCorrente;
using Questao5.Infrastructure.Database.CommandStore.Idempotencia;
using Questao5.Infrastructure.Database.CommandStore.Movimento;
using Questao5.Infrastructure.Sqlite;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// sqlite
builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();
builder.Services.AddSingleton<IContaCorrenteCommandStore, ContaCorrenteCommandStore>();
builder.Services.AddSingleton<IIdempotenciaCommandStore, IdempotenciaCommandStore>();
builder.Services.AddSingleton<IMovimentoCommandStore, MovimentoCommandStore>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Exercicios Softtek", Version = "v1" });
    c.EnableAnnotations();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1 Exercicio Softtek");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// sqlite
#pragma warning disable CS8602 // Dereference of a possibly null reference.
app.Services.GetService<IDatabaseBootstrap>().Setup();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

// middleware para tratamento de exceptions
app.UseMiddleware<ExceptionHandler>();

app.Run();

// Informações úteis:
// Tipos do Sqlite - https://www.sqlite.org/datatype3.html


