using Microsoft.AspNetCore.Mvc;
using RoyalState.Infrastructure.Identity;
using RoyalState.WebApi.Extensions;
using WealthMind.Core.Application;
using WealthMind.Infrastructure.Identity;
using WealthMind.Infrastructure.Persistence;
using WealthMind.Infrastructure.Shared;
using WealthMind.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

//PackagesInstaler.PackageDownload();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json"));
}).ConfigureApiBehaviorOptions(options =>
{
    options.SuppressInferBindingSourcesForParameters = true;
    options.SuppressMapClientErrors = true;
});

builder.Services.ConfigureCors();
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddIdentityInfrastructureForApi(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerExtension();
builder.Services.AddApiVersioningExtension();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
var app = builder.Build();

await app.Services.AddIdentitySeeds();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSwagggerExtension();
app.UseErrorHandlingMiddleware();
app.UseHealthChecks("/health");
app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

await app.RunAsync();
