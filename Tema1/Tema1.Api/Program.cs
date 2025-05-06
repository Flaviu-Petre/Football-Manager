using Microsoft.AspNetCore.Mvc;
using Tema1.Core;
using Tema1.Database;
using Tema1.Infrastructure.Config;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
//In loc de builder.Services.AddOpenApi(); a trebuit sa pun codul de la linia 11-17
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Football API", Version = "v1" });
});


builder.Services.AddAuthorization();
builder.Services.AddRepositories();
builder.Services.AddServices();

var app = builder.Build();


AppConfig.Init(app.Configuration);

//Am modificat aici
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "OpenAPI V1");
    });
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();