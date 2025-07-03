using TCMA.API.Infrastructure.Cache;
using TCMA.API.Infrastructure.Middleware;
using TCMA.API.Infrastructure.RateLimit;
using TCMA.API.Infrastructure.Extensions;
using TCMA.BLL;
using TCMA.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    CacheConfig.ConfigureCacheProfiles(options, builder.Configuration);
});

builder.Services.AddCorsPolicies();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddConfiguredRateLimiter(builder.Configuration);

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

app.UseCors(CorsConfig.PolicyName);

app.UseHttpsRedirection();

app.UseConfiguredRateLimiter();

app.UseAuthorization();

app.MapControllers();

app.UseResponseCaching();

app.Run();
