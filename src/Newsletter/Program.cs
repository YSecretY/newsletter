using Newsletter.Articles.Presentation;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddArticlesApi(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();