using Microsoft.EntityFrameworkCore;
using RecallSystem.Application.Interfaces;
using RecallSystem.Application.Services;
using RecallSystem.Domain.Interfaces;
using RecallSystem.Infrastructure.Data;
using RecallSystem.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RecallDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection")));

builder.Services.AddScoped<IRecallRepository, RecallRepository>();
builder.Services.AddScoped<IRecallService, RecallService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder.WithOrigins("http://localhost:5173")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});



var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<RecallDbContext>();
    DatabaseSeeder.SeedDatabase(dbContext);
}

// Habilitar arquivos estáticos
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseCors("AllowReactApp"); // Certifique-se de que o CORS está ativo antes de Authorization
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html");

// Certifique-se de que o Swagger está desativado
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.Run();
