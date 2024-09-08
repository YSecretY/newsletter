using Newsletter;
using Newsletter.Articles.Api;
using Newsletter.Extensions;
using Newsletter.Shared.Application;
using Newsletter.Shared.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSharedInfrastructure()
    .AddSharedApplication([Newsletter.Articles.Application.AssemblyReference.Assembly])
    .AddArticlesApi(builder.Configuration);

builder.Services
    .AddProblemDetails()
    .AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.MapControllers();

app.Run();