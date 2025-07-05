using System.Text.Json.Serialization;
using TCMA.API.Infrastructure.Extensions;
using TCMA.API.Infrastructure.Middleware;
using TCMA.API.Infrastructure.RateLimit;
using TCMA.BLL;
using TCMA.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddCorsPolicies();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddConfiguredRateLimiter(builder.Configuration);
builder.Services.AddGzipResponseCompression();

builder.Services
    .AddBLL(builder.Configuration)
    .AddDAL(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<GlobalErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(CorsExtension.PolicyName);

app.UseHttpsRedirection();

app.UseConfiguredRateLimiter();

app.UseAuthorization();

app.UseResponseCompression();

app.MapControllers();

app.Run();
