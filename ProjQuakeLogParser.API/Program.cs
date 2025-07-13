using ProjQuakeLogParser.IOC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.  
builder.Services.AddControllers();

// Register your services from the IOC container  
DependencyContainer.RegisterServices(builder.Services);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi  
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.  
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/openapi/v1.json", "ProjQuakeLogParser API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
